var i = 2;
var q = 2;
var subject;
function duplicate(sub) {    
    subject = sub;
    var original = document.getElementById('partA');
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
    h4.setAttribute('readonly', 'readonly');
    h4.value = 'Вопрос A' + q++;
    h4.className = "inp-lable";
    let h4name = "QuestionList[" + idq + "].type";
    h4.setAttribute('name', h4name);

    let inpDel = document.createElement('button');
    inpDel.className = "pressed-button2 fa fa-trash";
    inpDel.setAttribute('style','margin-left:860px');
    inpDel.id = i;
    inpDel.onclick = function () {
        del(inpDel);
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
    h42.setAttribute('style', 'margin: 0 10px 20px 190px');
    h42.innerText = "Ответы"
       
    let inpAdd = document.createElement('button');
    inpAdd.className = "pressed-button2 fa fa-plus-circle";
    inpAdd.id = 'v' + i;
    inpAdd.onclick = function () {
        return add(inpAdd,subject);
    }
    inpAdd.setAttribute('style', 'margin-left:10px');

    div3.appendChild(h4);
    div3.appendChild(inpDel);
    div4.appendChild(inp);
    div5.appendChild(h42);
    div5.appendChild(inpAdd);

    div2.appendChild(div3);
    div2.appendChild(div4);
    div2.appendChild(file);
    div2.appendChild(br);
    div2.appendChild(br2);
    div2.appendChild(div5);
    div.appendChild(div2);
    original.appendChild(div);
    return add(inpAdd,subject);    

}


function del(button) {
    let l = button.id;
    div = document.getElementById('div' + l);
    div.parentNode.removeChild(div);
    //q--;
    //i--;
}


var k = 1;
var singl = 0;
function add(button, subject) {

    let currId = button.id.slice(1);
    let questId = Number(currId) - 1;
    let id = 'di' + button.id;
    var parent = document.getElementById(id);
   
    let div = document.createElement('div');
    div.className = 'count'
    div.id = "answ" + ++k;

    let inp = document.createElement('input');
    inp.className = "form-control";
    inp.setAttribute('type', 'text');
    inp.setAttribute('style', 'float: left; display: inline-block; width: 65%; margin:5px 23px 0px 190px');
    inp.id = "a" + k;


    let arr = parent.getElementsByClassName('count');
    let count;
    if (arr.length !=0) {
        let item = arr.item(arr.length - 1);
        let idelem = item.getAttribute('id').slice(4);
        idelem = 'a' + idelem;
        let child = document.getElementById(idelem);
        let value = child.getAttribute('name');
        let arr2 = value.split('.');
        let elem = arr2[1];
        count = Number(elem.slice(elem.indexOf("[") + 1, elem.indexOf("]"))) + 1;
        let nameId = 'QuestionList[' + questId + '].AnswerList[' + count + '].text';
        inp.setAttribute('name', nameId);
    }
    else {
        count = 0;
        let nameId = 'QuestionList[' + questId + '].AnswerList[' + count + '].text';
        inp.setAttribute('name', nameId);
    }

    let label2 = document.createElement('label');
    label2.className = "checkbox-second";
    label2.setAttribute('style', 'margin-right:24px; margin-top:10px');
    let check;
    let checkId;
    if (subject == "Русский язык" || subject == "Беларуская мова" || subject == "Английский язык") {
        check = document.createElement('input');
        check.setAttribute('type', 'checkbox')
        check.setAttribute('value', 'true')
        check.id = "ch" + k;
        checkId = 'QuestionList[' + questId + '].AnswerList[' + count + '].isRight';
        check.setAttribute('name', checkId);

    }
    else {
        check = document.createElement('input');
        check.setAttribute('type', 'checkbox')
        check.setAttribute('value', 'true')
        check.id = "ch" + k;
        checkId = 'QuestionList[' + questId + '].AnswerList[' + count + '].isRight';
        check.setAttribute('name', checkId);
        check.onclick = function () {
            return switchCheck(check);
        }
    }

    let div6 = document.createElement('div');
    div6.className = "checkbox-second__text";
    div6.innerHTML = "Верно";

        let inpDel = document.createElement('button');
        inpDel.className = "pressed-button2 fa fa-trash";
        inpDel.id = 'w' + k;
        inpDel.onclick = function () {
            delAnsw(inpDel);
        }
        inpDel.setAttribute('style', 'margin-top:5px')
    

    label2.appendChild(check);
    label2.appendChild(div6)
    div.appendChild(inp);
    div.appendChild(label2);
    if (arr.length != 0) {
        div.appendChild(inpDel);
        }
    parent.appendChild(div);
    return false;

}


function delAnsw(button) {
    let k = button.id;
    div = document.getElementById('ans' + k);
    div.parentNode.removeChild(div);

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


var curElem;
var prevElem;
var isFirst = true;
function switchCheck(elem) {
    var blocElems = document.querySelectorAll('[name*="' + elem.getAttribute('name').split('.')[0] +'"][type="checkbox"]')
   
    for (var i = 0; i < blocElems.length; i++) {
       
            curElem = elem.id;
            var curElement = document.getElementById(curElem);
            prevElement = curElement;
            if (blocElems[i].checked == true && curElement != blocElems[i]) {
                prevElem = blocElems[i].id;
                prevElement = document.getElementById(prevElem)

                curElement.checked = true;
                prevElement.checked = false;
            }
            
    }
}

function switchCheck2(elem) {
    var blocElems = document.querySelectorAll('[class*="' + elem.getAttribute('class').split('.')[0] + '"][type="checkbox"]')

    for (var i = 0; i < blocElems.length; i++) {

        curElem = elem.id;
        var curElement = document.getElementById(curElem);
        prevElement = curElement;
        if (blocElems[i].checked == true && curElement != blocElems[i]) {
            prevElem = blocElems[i].id;
            prevElement = document.getElementById(prevElem)

            curElement.checked = true;
            prevElement.checked = false;
            
        }
        
    }
}

