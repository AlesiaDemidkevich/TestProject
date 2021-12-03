var i = 1;
function duplicate() {
    var original = document.getElementById('container')
    let div = document.createElement('div');
    div.className = "form-group border border-5 rounded p-2";
    div.id = "div" + ++i;

    let lab = document.createElement('label');
    lab.className = "control-label";
    lab.setAttribute('asp-for', 'QuestionText');
    lab.setAttribute('style', 'margin:7px 5px 7px 0px');
    lab.innerHTML = 'Question ' + i;

    let divCh = document.createElement('div');
    let idq = i - 1;

    let file = document.createElement('input');
    let fileId = 'QuestionList[' + idq + '].imageUrl';
    file.setAttribute('name', fileId)
    file.type = "file";    
    file.size = "60";

    let inp = document.createElement('input');
    inp.className = "form-control";
    inp.setAttribute('type', 'text');    
    let nameId = 'QuestionList[' + idq + '].text';
    inp.setAttribute('name', nameId)
    inp.setAttribute('style', 'float:left; display:inline-block; width:91%; margin:0px 20px 10px 0px;');
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
    div.appendChild(file);
    div.appendChild(document.createElement('br'));
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
    inp.setAttribute('style', 'float: left; display: inline-block; width: 78%; margin:5px 20px 0px 65px');
    inp.id = "a" + k;
    //if (singl == 0) {
        //let nameId = 'QuestionList[' + questId + '].AnswerList[' + 0 + '].text';
        //inp.setAttribute('name', nameId);
        //let checkId = 'QuestionList[' + questId + '].AnswerList[' + 0 + '].isRight';
        //check.setAttribute('name', checkId);
        //singl++;
    //}
    //else {
        let count = parent.getElementsByClassName('count').length;
        let nameId = 'QuestionList[' + questId + '].AnswerList[' + count + '].text';
        inp.setAttribute('name', nameId)
        let checkId = 'QuestionList[' + questId + '].AnswerList[' + count + '].isRight';
        check.setAttribute('name', checkId);
       
    //}

    let labC = document.createElement('label');
    labC.className = "control-label";
    labC.setAttribute('style', 'margin:7px 5px 7px 3px');
    labC.innerHTML = 'isRight';

    let inpDel = document.createElement('button');
    inpDel.className = "btn btn-outline-danger";
    inpDel.id = 'w' + k;
    inpDel.onclick = function () {
        delAnsw(inpDel);
    }
    inpDel.setAttribute('style', 'margin-top:5px')
    inpDel.innerText = "Delete";
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

function getAllAnswerForQuestion(parent,qId) {
    let count = parent.getElementsByClassName('count').length;
    for (let i = 0; i < count; i++) {
        div = parent.getElementsByClassName('count');
        let nameId = 'QuestionList[' + qId + '].AnswerList[' + i + '].text';        
        div[i].setAttribute('name', nameId);
    }
    return false;
}



    //var i = 0;
    //$('#addQuestion').click(function () {
    //    i++;
    //    var html2Add = "< div class='form-group'' id = 'div1; >" +
    //        "<label class='control-label' style='margin:7px 5px 7px 0px'>Question 1</label>" +
    //        "<div>" +
    //            "<input type='text'' name='QuestionList["+i+"].text'' class='form-control' id='duplic1' style='float:left; display:inline-block; width:91%; margin-right:20px;' />"
    //            "<button onclick='del(this)' class='btn btn - outline - danger' id='1'>Delete</button>" +
    //         "</div>" +
    //                "<label class='control - label' style='margin: 7px 5px 7px 0px'>Answers</label>" +
    //                "<button onclick='return add(this)' class='btn btn-outline-info' id='v1' style='margin:7px 5px 7px 5px'>Add</button>" +
    //                "<div id='answ1'' class='count'>" +
    //                "<input type='text' name='QuestionList["+i+"].AnswerList[0].text' class='form - control' id='a1' style='float: left; display: inline - block; width: 85 %; margin: 5px 20px 0px 65px' />" +
    //                "<button onclick='return delAnsw(this)' class='btn btn - outline - danger' id='w1' style='margin - top: 5px'>Delete</button>" 
    //    "</div ></div > ";
    //    $('#container').append(html2Add);
    //})
