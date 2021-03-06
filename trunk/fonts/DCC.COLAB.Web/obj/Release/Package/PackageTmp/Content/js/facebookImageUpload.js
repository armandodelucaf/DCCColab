﻿if (XMLHttpRequest.prototype.sendAsBinary === undefined) {
    XMLHttpRequest.prototype.sendAsBinary = function (string) {
        var bytes = Array.prototype.map.call(string, function (c) {
            return c.charCodeAt(0) & 0xff;
        });
        this.send(new Uint8Array(bytes).buffer);
    };
}

var PostaImagemNoFacebook = function (albumId, authToken, filename, mimeType, imageData, message) {
    // this is the multipart/form-data boundary we'll use
    var boundary = '----ThisIsTheBoundary1234567890';

    // let's encode our image file, which is contained in the var
    var formData = '--' + boundary + '\r\n'
    formData += 'Content-Disposition: form-data; name="source"; filename="' + filename + '"\r\n';
    formData += 'Content-Type: ' + mimeType + '\r\n\r\n';
    for (var i = 0; i < imageData.length; ++i) {
        formData += String.fromCharCode(imageData[i] & 0xff);
    }
    formData += '\r\n';
    formData += '--' + boundary + '\r\n';
    formData += 'Content-Disposition: form-data; name="message"\r\n\r\n';
    formData += message + '\r\n'
    formData += '--' + boundary + '--\r\n';

    var xhr = new XMLHttpRequest();
    xhr.open('POST', 'https://graph.facebook.com/' + albumId + '/photos?access_token=' + authToken, true);
    xhr.onload = xhr.onerror = function () {
        console.log(xhr.responseText);
    };
    xhr.setRequestHeader("Content-Type", "multipart/form-data; boundary=" + boundary);
    xhr.sendAsBinary(formData);
}