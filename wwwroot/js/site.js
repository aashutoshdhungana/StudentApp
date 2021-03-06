// Code for js form validation
function formValidation(id) {
    try {
        var form = document.forms[id];
        removeValMessage();
        form['Email'].value = form['Email'].value.toLowerCase();
        if (form['FirstName'].value === "") {
            console.log("You surely have a first name");
            document.getElementById('FirstNameVal').innerHTML = 'First Name cannot be empty';
            form['FirstName'].focus();
            return false;
        }
        if (form['LastName'].value === "") {
            document.getElementById('LastNameVal').innerHTML = "Last Name cannot be empty";
            form['LastName'].focus();
            return false;
        }
        if (form['MiddleName'].value.length > 1) {
            document.getElementById('MiddleNameVal').innerHTML = "Middle Name should be a single character";
            form['MiddleName'].focus();
            return false;
        }
        if (form['Email'].value === "") {
            console.log("Even dinosaurs have Email");
            document.getElementById('EmailVal').innerHTML = "Email cannot be empty";
            form['Email'].focus();

            return false;
        }

        if (!emailValidation(form['Email'].value)) {
            document.getElementById('EmailVal').innerHTML = "Enter a valid email";
            form['Email'].focus();
            return false;
        }

        if (form['PhoneNumber'].value === "") {
            document.getElementById('PhoneNumberVal').innerHTML = "Phone number cannot be empty";
            form['PhoneNumber'].focus();
            return false;
        }

        if (!phoneNumberValidation(form['PhoneNumber'].value)) {
            document.getElementById('PhoneNumberVal').innerHTML = "Enter a valid mobile number";
            form['PhoneNumber'].focus();
            return false;
        }
        return true;
    } catch (e) {
        console.error(e);
        return false;
    }
}

function emailValidation(str) {
    return str.match(/\w.\w@\w+\.\w+/);
}

function phoneNumberValidation(str) {
    return str.match(/\d\d\d\d\d\d\d\d\d\d/);
}

function removeValMessage() {
    var spans = document.querySelectorAll('.form-group span');
    for (var i = 0; i < spans.length; i++) {
        spans[i].innerHTML = "";
    }
    return;
}

function readmore() {
    let about = document.getElementById("about");
    let link = document.getElementById("seemore-link");
    if (about.clientHeight > 200) {
        about.style.height = '200px';
        link.innerHTML = 'Read more';
    }
    else {
        about.style.height = 'fit-content';
        link.innerHTML = 'Show Less';
    }
}