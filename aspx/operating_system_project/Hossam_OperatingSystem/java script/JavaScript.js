// check if the last char of the  value is number , if not cut's the char  
//for the best performance use "onkeyup="isnumber(this.value,this.id);" onkeydown="isnumber(this.value,this.id);"
function isnumber(value, id) {
    if (value.lastIndexOf(' ') != -1)
    { document.getElementById(id).value = value.replace(' ', ''); return; }
    if (!isNaN(value.slice(-1))) { return; }
    else { document.getElementById(id).value = value.slice(0, value.length - 1); }
}

//preventing enter from submitting
function stopRKey(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
    if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
}
document.onkeypress = stopRKey;


//check if the text boxes has data ,if not  the submit button is disabled
function checkAllTextboxes()
{
    if ( document.getElementById("ButtonSubmit").disabled == true) {

    document.getElementById("ButtonSubmit").disabled = true;
    }
    var textbox = document.getElementsByTagName("input");
    for (var i = 0; i < textbox.length; i++) {
        if (!( textbox[i].value != '' && textbox[i].value[0] != ' ') )
        { return; }

    }
    document.getElementById("ButtonSubmit").disabled = false;
}


//check if the text areas has data  and the length more than 5 digits,if not  the submit button is disabled
function checkAllTextAreas()
{
    document.getElementById("ButtonSubmit").disabled = true;
    var textarea = document.getElementsByTagName("textarea");
    for (var i = 0; i < textarea.length; i++) {
        if (!(textarea[i].value != '' && textarea[i].value[0] != ' ' && textarea[i].value.length>5))
        { return; }

    }
    document.getElementById("ButtonSubmit").disabled = false;
}

//check if the text boxes has data ,if not  the submit button is disabled
function check_textboxes() {

    document.getElementById("ButtonSubmit").disabled = true;
    if (
        document.getElementById("TextBoxId").value.length > 0 &&
        document.getElementById("TextBoxName").value.length > 0 &&
        document.getElementById("TextBoxField").value.length > 0 &&
        document.getElementById("TextBoxUserName").value.length > 0 &&
        document.getElementById("TextBoxPassword").value.length > 0
        )
    { document.getElementById("ButtonSubmit").disabled = false; }

}

function add_employee_check_textboxes() {

    document.getElementById("ButtonSubmit").disabled = true;
    if (
        document.getElementById("TextBoxId").value.length > 0 &&
        document.getElementById("TextBoxName").value.length > 0 &&
        document.getElementById("TextBoxUserName").value.length > 0 &&
        document.getElementById("TextBoxPassword").value.length > 0
        )
    { document.getElementById("ButtonSubmit").disabled = false; }

}

//check if there is data in the user name and password
function check_password() {

    if (document.getElementById("TextBoxName").value.match(/\w/) && document.getElementById("TextBoxPassword").value.match(/\w/)) {
        document.getElementById("Button_login").disabled = false;
        if (document.getElementById("ErrorLabel").textContent != null) {
            document.getElementById("ErrorLabel").textContent = "";
        }

    }
    else {
        
        document.getElementById("Button_login").disabled = true;
    }
}

//confirmation when submit button clicked
function confirmation(text) {
    if (confirm('Are you sure you want to  ' + text))
    { return true; }
    else
    { return false; }
}


