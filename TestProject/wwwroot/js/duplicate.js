var i = 1;
var countSelect = document.getElementsByClassName('btnch').length;
function duplicate() {
    
    var original = document.getElementById('container')
    let div = document.createElement('div');
    div.className = "form-group border border-5 rounded p-2";
    div.id = "div" + ++i;

    let lab = document.createElement('label');
    lab.className = "control-label";
    lab.setAttribute('asp-for', 'QuestionText');
    lab.setAttribute('style', 'display:inline-block; margin:7px 5px 7px 0px;');
    lab.innerHTML = 'Вопрос ' + i;

    let divCh = document.createElement('div');
    let idq = i - 1;

    let file = document.createElement('input');
    let fileId = 'QuestionList[' + idq + '].imageUrlFile';
    file.setAttribute('name', fileId)
    file.type = "file";
    file.size = "60";

   


    let inp = document.createElement('input');
    inp.className = "form-control";
    inp.setAttribute('type', 'text');
    let nameId = 'QuestionList[' + idq + '].text';
    inp.setAttribute('name', nameId);
    inp.setAttribute('style', 'float:left; display:inline-block; width:90%; margin:0px 16px 10px 0px;');
    inp.id = "duplic" + i;

    let labA = document.createElement('label');
    labA.className = "control-label";
    labA.setAttribute('asp-for', 'AnswerText');
    labA.setAttribute('style', 'margin:7px 5px 7px 0px;');
    labA.innerHTML = 'Ответы';

    let inpAdd = document.createElement('button');
    inpAdd.className = "btn btn-outline-info";
    inpAdd.id = 'v' + i;
    inpAdd.onclick = function () {
        return add(inpAdd);
    }
    inpAdd.setAttribute('style', 'margin:7px 5px 7px 5px');
    inpAdd.innerText = "Добавить"

    let sel = document.createElement("select");
    sel.id = "t" + i;
    sel.options[0] = new Option('Часть A', 'A');
    sel.options[1] = new Option('Часть B', 'B');
    let name = 'QuestionList[' + idq + '].Type';
    sel.setAttribute('name', name);
    sel.setAttribute('class', 'btnch');
    //sel.onchange = function () {
    //    return setListener(inpAdd);
    //}

    let inpDel = document.createElement('button');
    inpDel.className = "btn btn-outline-danger";
    inpDel.id = i;
    inpDel.onclick = function () {
        del(inpDel);
    }
    inpDel.innerText = "Удалить"
    divCh.appendChild(inp);
    divCh.appendChild(inpDel);
    div.appendChild(lab);
    div.appendChild(sel);
    div.appendChild(divCh);
    div.appendChild(file);
    div.appendChild(document.createElement('br'));
    div.appendChild(labA);
    div.appendChild(inpAdd);
    original.appendChild(div);

    
    return add(inpAdd);
    

}

//function setListener(button) {
//    console.log("A");
//    return false;
//}


function del(button) {
    let l = button.id;
    div = document.getElementById('div' + l);
    div.parentNode.removeChild(div);
}


var k = 1;
var singl = 0;
function add(button) {

    let currId = button.id.substr(1, 1);
    let questId = currId - 1;
    let id = 'di' + button.id;
    var parent = document.getElementById(id);

    let div = document.createElement('div');
    div.className = 'count'
    div.id = "answ" + ++k;

    let check = document.createElement('input');
    check.setAttribute('type', 'checkbox')
    check.setAttribute('value', 'true')

    let inp = document.createElement('input');
    inp.className = "form-control";
    inp.setAttribute('type', 'text');
    inp.setAttribute('style', 'float: left; display: inline-block; width: 77%; margin:5px 20px 0px 65px');
    inp.id = "a" + k;
    let count = parent.getElementsByClassName('count').length;
    let nameId = 'QuestionList[' + questId + '].AnswerList[' + count + '].text';
    inp.setAttribute('name', nameId)
    let checkId = 'QuestionList[' + questId + '].AnswerList[' + count + '].isRight';
    check.setAttribute('name', checkId);

    let labC = document.createElement('label');
    labC.className = "control-label";
    labC.setAttribute('style', 'margin:7px 5px 7px 8px');
    labC.innerHTML = 'Верно';

    let inpDel = document.createElement('button');
    inpDel.className = "btn btn-outline-danger";
    inpDel.id = 'w' + k;
    inpDel.onclick = function () {
        delAnsw(inpDel);
    }
    inpDel.setAttribute('style', 'margin-top:5px')
    inpDel.innerText = "Удалить";
    div.appendChild(inp);
    div.appendChild(check);
    div.appendChild(labC);
    div.appendChild(inpDel);
    parent.appendChild(div);
    return false;

}


function delAnsw(button) {
    let k = button.id;
    div = document.getElementById('ans' + k);
    div.parentNode.removeChild(div);

    //getAllAnswerForQuestion(div.parentNode, div.parentNode.id);

    return false;
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


