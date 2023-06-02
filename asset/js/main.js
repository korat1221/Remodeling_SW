
let tmClicked = null, command = null;
var gMainTree = new MainTree((data) => {
    let tm = new Date();

    if (!command || command !== data || !tmClicked || (tm - tmClicked) > 1000) {
        let o = JSON.parse(data);

        if (!o) {
            o = { formID: parseInt(data) };
        }

        window.chrome.webview.postMessage(JSON.stringify(o));

        command = data;
    }
    tmClicked = tm;
});
