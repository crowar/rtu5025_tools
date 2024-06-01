export class UploadModal {
    static observer;

    static async GetFileData(id) {
        const target = document.getElementById(id);
        const filesArray = Array.from(target.files);
        return Promise.all(filesArray.map(this.fileToDataURL));
    }
    
    static fileToDataURL(file) {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onerror = () => {
                reader.abort();
                reject(new DOMException('Error occurred reading file ' + file));
            };
            reader.onload = () => resolve(reader.result);
            reader.readAsDataURL(file);
        });
    }
    
    static InvokeClick(id) {
        const elem = document.getElementById(id);
        if (typeof elem.onclick === "function") {
            elem.onclick.apply(elem);
        }
        elem.click();
    }
}

window.UploadModal = UploadModal;