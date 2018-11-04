import $ from 'jquery';

export function sendForm(url, type, form, callback = undefined) {
	$.ajax({
		url: url,
		type: type,
		enctype: 'multipart/form-data',
		processData: false,
		contentType: false,
		data: new FormData(form),
		cache: false,
		success: function (data) {
			if (callback !== undefined) {
				return callback(data);
			}
		},
		error: function (data) {
			console.log(data);
		},
	});
}