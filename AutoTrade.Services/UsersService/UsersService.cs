using System;
using System.Linq;
using System.Security.Cryptography;
using AutoTrade.Core.JsonModels;
using AutoTrade.Db;
using AutoTrade.Db.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AutoTrade.Services.UsersService
{
	public class UsersService : BaseService, IUsersService
	{
		public UsersService(AppDbContext dbContext) : base(dbContext)
		{ }

		public bool CreateAccount(UserJsonModel model)
		{
			bool isNewUser = DbContext.Users
				.SingleOrDefault(u => u.Email == model.Email) == null;
			if (isNewUser)
			{
				var user = (User)this.Map(model, new User());
				user.Hash = HashPassword(model.Password);
				DbContext.Users.Add(user);
				int rowsAffected = DbContext.SaveChanges();
				isNewUser = rowsAffected > 0 ? true : false;
			}
			return isNewUser;
		}

		public bool IsPasswordValid(string email, string password)
		{
			var user = DbContext.Users.SingleOrDefault(u => u.Email == email);
			return VerifyHashedPassword(user.Hash, password);
		}

		private byte[] HashPassword(string password)
		{
			const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1;
			const int Pbkdf2IterCount = 1000;
			const int Pbkdf2SubkeyLength = 256 / 8;
			const int SaltSize = 128 / 8;

			byte[] salt = new byte[SaltSize];

			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

			var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
			outputBytes[0] = 0x00; // format marker
			Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
			Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
			return outputBytes;
		}

		private bool VerifyHashedPassword(byte[] hashedPassword, string password)
		{
			const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1;
			const int Pbkdf2IterCount = 1000;
			const int Pbkdf2SubkeyLength = 256 / 8;
			const int SaltSize = 128 / 8;

			if (hashedPassword.Length != 1 + SaltSize + Pbkdf2SubkeyLength)
			{
				return false;
			}

			byte[] salt = new byte[SaltSize];
			Buffer.BlockCopy(hashedPassword, 1, salt, 0, salt.Length);

			byte[] expectedSubkey = new byte[Pbkdf2SubkeyLength];
			Buffer.BlockCopy(hashedPassword, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

			byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);
			return ByteArraysEqual(actualSubkey, expectedSubkey);
		}

		private bool ByteArraysEqual(byte[] a, byte[] b)
		{
			if (a == null && b == null)
			{
				return true;
			}
			if (a == null || b == null || a.Length != b.Length)
			{
				return false;
			}
			var areSame = true;
			for (var i = 0; i < a.Length; i++)
			{
				areSame &= (a[i] == b[i]);
			}
			return areSame;
		}
	}
}
