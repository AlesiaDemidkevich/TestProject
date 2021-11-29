var i = 0;
function duplicate() {
    //var original = document.getElementById('duplic'),
    //    clone = original.cloneNode(true); // "deep" clone
    //clone.id = "duplic" + ++i; // there can only be one element with an ID
    //document.append(clone);

    let div = document.createElement('div');
    div.className = "alert";
    div.innerHTML = "<strong>Всем привет!</strong> Вы прочитали важное сообщение.";

    document.body.append(div);
}