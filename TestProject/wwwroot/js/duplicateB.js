var i = 2;
var p = 2;
var k = 2;
var singl = 0;
function duplicateB() {    
    var original = document.getElementById('partB');    
    
    let div = document.createElement('div');
    div.className = "form-group";

    let div2 = document.createElement('div');
    div2.className = "block";
    div2.id = "div" + ++i;
    let idq = i - 1;
    let div3 = document.createElement('div');
    div3.setAttribute('style', 'display:flex; margin-bottom:10px');

    let h4 = document.createElement('input');
    h4.type = "text";
    h4.className = "inp-lable";
    h4.setAttribute('readonly', 'readonly');
    h4.value = 'Вопрос B' + p++;
    let h4name = "QuestionList[" + idq + "].type";
    h4.setAttribute('name', h4name);

    let inpDel = document.createElement('button');
    inpDel.className = "pressed-button2 fa fa-trash";
    inpDel.setAttribute('style', 'margin-left:860px');
    inpDel.id = i;
    inpDel.onclick = function () {
        delB(inpDel);
    }

    let div4 = document.createElement('div');

    let inp = document.createElement('input');
    inp.className = "form-control";
    inp.setAttribute('type', 'text');
    let nameId = 'QuestionList[' + idq + '].text';
    inp.setAttribute('name', nameId);
    inp.setAttribute('style', 'float:left; display:inline-block; width:100%; margin:0px 16px 10px 0px;');
    inp.id = "duplic" + i;

    let file = document.createElement('input');
    let fileId = 'QuestionList[' + idq + '].imageUrlFile';
    file.setAttribute('name', fileId)
    file.type = "file";
    file.size = "60";

    let br = document.createElement('br');
    let br2 = document.createElement('br');

    let div5 = document.createElement('div');
    div5.setAttribute('style','display:flex; margin-bottom:10px');

    let h42 = document.createElement('h4');
    h42.setAttribute('style', 'margin: 0 10px 10px 190px');
    h42.innerText = "Ответ";
     
    let div8 = document.createElement('div');
    div8.className = 'count'
    div8.id = "answ" + ++k;
    div8.setAttribute('style','display:flex');

    //let currId = div8.id.substr(4, 1);
    //let questId = currId-1;

    let inp2 = document.createElement('input');
    inp2.className = "form-control";
    inp2.setAttribute('type', 'text');
    inp2.setAttribute('style', 'width:650px; margin:0px 15px 10px 0px');
    inp2.id = "a" + k;
    let count = div2.getElementsByClassName('count').length;
    let nameId2 = 'QuestionList[' + idq + '].AnswerList[' + 0 + '].text';
    inp2.setAttribute('name', nameId2);

    let label2 = document.createElement('label');
    label2.className = "checkbox-second";
    label2.setAttribute('style', 'margin-top:10px');


    let check = document.createElement('input');
    check.setAttribute('type', 'checkbox')
    check.setAttribute('value', 'true')
    check.setAttribute('checked','checked');
    check.id = "ch" + k;
    let checkId = 'QuestionList[' + idq + '].AnswerList[' + 0 + '].isRight';
    check.setAttribute('name', checkId);
    check.onclick = function () {
       return false;
    }

    let div6 = document.createElement('div');
    div6.className = "checkbox-second__text";
    div6.innerHTML = "Верно";

    
    label2.appendChild(check);
    label2.appendChild(div6)
    div8.appendChild(inp2);
    div8.appendChild(label2);    
    div3.appendChild(h4);
    div3.appendChild(inpDel);
    div4.appendChild(inp);
    div5.appendChild(h42);
    div5.appendChild(div8);

    div2.appendChild(div3);
    div2.appendChild(div4);
    div2.appendChild(file);
    div2.appendChild(br);
    div2.appendChild(br2);
    div2.appendChild(div5);
    div.appendChild(div2);
    original.appendChild(div);   

    return false;
}


function delB(button) {
    let l = button.id;
    div = document.getElementById('div' + l);
    div.parentNode.removeChild(div);
    //p--;
    //i--;
}


function getAllAnswerForQuestion(parent, qId) {
    let count = parent.getElementsByClassName('count').length;
    for (let i = 0; i < count; i++) {
        div = parent.getElementsByClassName('count');
        let nameId = 'QuestionList[' + qId + '].AnswerList[' + i + '].text';
        div[i].setAttribute('name', nameId);
    }
    return false;
}


