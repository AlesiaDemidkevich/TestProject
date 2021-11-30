var i = 1;
function duplicate() {
    var original = document.getElementById('container')
    let div = document.createElement('div');
    div.className = "form-group";
    div.id = "div" + ++i;

    let lab = document.createElement('label');
    lab.className = "control-label";
    lab.setAttribute('asp-for', 'QuestionText');
    lab.setAttribute('style', 'margin:7px 5px 7px 0px');
    lab.innerHTML = 'Question ' + i;

    let divCh = document.createElement('div');

    let inp = document.createElement('input');
    inp.className = "form-control";
    inp.setAttribute('type', 'text');
/*    inp.setAttribute('asp-for', 'QuestionText');*/
    inp.setAttribute('name','text')
    inp.setAttribute('style', 'float:left; display:inline-block; width:91%; margin-right:20px;');
    inp.id = "duplic" + i;

    let labA = document.createElement('label');
    labA.className = "control-label";
    labA.setAttribute('asp-for', 'AnswerText');
    labA.setAttribute('style', 'margin:7px 5px 7px 0px');
    labA.innerHTML = 'Answers';

    let inpAdd = document.createElement('button');
    inpAdd.className = "btn btn-outline-info";
    inpAdd.id = 'v' + i;
    inpAdd.onclick = function () {
        return add(inpAdd);
    }
    inpAdd.setAttribute('style', 'margin:7px 5px 7px 5px');
    inpAdd.innerText = "Add"

    let inpDel = document.createElement('button');
    inpDel.className = "btn btn-outline-danger";
    inpDel.id = i;
    inpDel.onclick = function () {
        del(inpDel);
    }
    inpDel.innerText = "Delete"
    divCh.appendChild(inp);
    divCh.appendChild(inpDel);
    div.appendChild(lab);
    div.appendChild(divCh);
    div.appendChild(labA);
    div.appendChild(inpAdd);
    original.appendChild(div);

    return add(inpAdd);

}

function del(button) {
    let l = button.id;
    div = document.getElementById('div' + l);
    div.parentNode.removeChild(div);
}


var k = 1;
function add(button) {
    let id = 'di' + button.id
    var parent = document.getElementById(id)
    let div = document.createElement('div');
    div.id = "answ" + ++k;

    let inp = document.createElement('input');
    inp.className = "form-control";
    inp.setAttribute('type', 'text');
    //inp.setAttribute('asp-for', 'AnswerText');
    let ind = k - 1;
    let nameId = 'AnswerList[' + ind + '].text';
    inp.setAttribute('name', nameId)
    inp.setAttribute('style', 'float: left; display: inline-block; width: 85%; margin:5px 20px 0px 65px');
    inp.id = "a" + k;
    let inpDel = document.createElement('button');
    inpDel.className = "btn btn-outline-danger";
    inpDel.id = 'w' + k;
    inpDel.onclick = function () {
        delAnsw(inpDel);
    }
    inpDel.setAttribute('style', 'margin-top:5px')
    inpDel.innerText = "Delete";
    div.appendChild(inp);
    div.appendChild(inpDel);
    parent.appendChild(div);
    return false;

}


function delAnsw(button) {
    let k = button.id;
    div = document.getElementById('ans' + k);
    div.parentNode.removeChild(div);
    return false;
}