

function validate_password() {

    let pass = document.getElementById('addPassword1').value;
    let confirm_pass = document.getElementById('addPassword2').value;
    if (pass != confirm_pass) {
        document.getElementById('addPasswordAlert').style.color = 'red';
        document.getElementById('addPasswordAlert').innerHTML
            = '☒ Use same password';
        document.getElementById('btnCreate').disabled = true;
        document.getElementById('btnCreate').style.opacity = (0.4);
    } else {
        document.getElementById('addPasswordAlert').style.color = 'green';
        document.getElementById('addPasswordAlert').innerHTML =
            '🗹 Password Matched';
        document.getElementById('btnCreate').disabled = false;
        document.getElementById('btnCreate').style.opacity = (1);
    }
}
function Add_Voter_Username_Validation() {
    var dataVoters = {
        username: document.getElementById("addUsername").value, 
    };

    $.ajax({
        type: "POST",
        url: "Load_Voters_Username_Validation_Add",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: dataVoters,
        success: function (response) { 
            if (response) {
                document.getElementById('addAlert').style.color = 'red';
                document.getElementById('addAlert').innerHTML = 'Username already choosen';
                document.getElementById('btnCreate').disabled = true;
                document.getElementById('btnCreate').style.opacity = (0.4);
            }
            else {
                document.getElementById('addAlert').innerHTML = '';
                document.getElementById('btnCreate').disabled = false;
                document.getElementById('btnCreate').style.opacity = (1);
            }
        },
        error: function (err) {
            publish_Voters_Add_Error(err);
        }
    });
}
function AddNewVoters() {
    if (document.getElementById('addPassword1').value == "" ||
        document.getElementById('addPassword2').value == "") {

        document.getElementById('addAlert').style.color = 'red';
        document.getElementById('addAlert').innerHTML = 'Kindly fill required fields';
        return false;
    }  

    if (document.getElementById('addAcceptTerms').checked == false) {

        document.getElementById('addAlert').style.color = 'red';
        document.getElementById('addAlert').innerHTML = 'Kindly accept terms and conditions';
        return false;
    }

    if (document.getElementById('addFirstName').value == "") {

        document.getElementById('addAlert').style.color = 'red';
        document.getElementById('addAlert').innerHTML = 'First Name is required';
        return false;
    }

    if (document.getElementById('addLastName').value == "") {

        document.getElementById('addAlert').style.color = 'red';
        document.getElementById('addAlert').innerHTML = 'Last Name is required';
        return false;
    }
    if (document.getElementById('addUsername').value == "") {

        document.getElementById('addAlert').style.color = 'red';
        document.getElementById('addAlert').innerHTML = 'Username is required';
        return false;
    }
    Voters_Add();
}

function Voters_Add() {
    var dataVoters = {
        firstname: document.getElementById("addFirstName").value,
        lastname: document.getElementById("addLastName").value,
        username: document.getElementById("addUsername").value,
        password: document.getElementById("addPassword1").value,
        address: document.getElementById("addAddress").value,
        constituencyid: document.getElementById("ddlConstituency").value,
        isapproved : 0,
        photo: null,
    };

    $.ajax({
        type: "POST",
        url: "Load_Voters_Add",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: dataVoters,
        success: function (response) {
            publish_Voters_Add(response);
        },
        error: function (err) {
            publish_Voters_Add_Error(err);
        }
    });
} 

function publish_Voters_Add(response) {
    debugger; 
    $('#basicModal').modal('show');

    setTimeout(function () {
        window.location.href = "Login"; //will redirect to your blog page (an ex: blog.html)
    }, 5000); //will call the function after 2 secs.

}

function publish_Voters_Add_Error(err) {


}


function Load_Voters_Constituency_DropDown_Details() {

    $.ajax({
        type: "POST",
        url: "../ECIOfficials/Load_Candidates_Constituency_DropDown_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            Candidates_Constituency_DropDown_Load(response);
        },
        error: function (err) {

        }
    });
}
function Candidates_Constituency_DropDown_Load(response) {

    var ddlConstituency = document.getElementById("ddlConstituency");

    for (var i = 0; i < response.length; i++) {

        var addoption = document.createElement('option');
        var single = response[i];
        addoption.text = single.name;
        addoption.value = single.id;

        ddlConstituency.add(addoption, i);
    }
}