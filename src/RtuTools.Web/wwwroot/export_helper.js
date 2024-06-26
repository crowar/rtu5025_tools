"use strict";
function clickElement(element) {
    element.click();
}
function downloadFromUrl(options) {
    var _a;
    var anchorElement = document.createElement('a');
    anchorElement.href = options.url;
    anchorElement.download = (_a = options.fileName) !== null && _a !== void 0 ? _a : '';
    anchorElement.click();
    anchorElement.remove();
}
function downloadFromByteArray(options) {
    var url = typeof (options.byteArray) === 'string' ?
        "data:" + options.contentType + ";base64," + options.byteArray :
        URL.createObjectURL(new Blob([options.byteArray], { type: options.contentType }));
    downloadFromUrl({ url: url, fileName: options.fileName });
    if (typeof (options.byteArray) !== 'string')
        URL.revokeObjectURL(url);
}