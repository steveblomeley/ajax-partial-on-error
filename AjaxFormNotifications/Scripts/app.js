function onSuccess() {
    alert("success!");
}

function onFailure() {
    alert("Failure!");
}

function onJsonSuccess(data) {
    console.log(`success! ${data.Message}`);
    $("#my-target").prepend(data.ViewHtml);
}

function onJsonFailure(data) {
    console.log(`error! ${data.responseJSON.Message}`);
    $("#my-target").prepend(data.responseJSON.ViewHtml);
}