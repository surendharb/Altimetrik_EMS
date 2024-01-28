


function Login_Official_Click() {
    
    var dataLogin = {
        username: document.getElementById("officialUsername").value,
        password: document.getElementById("officialPassword").value,
    }; 
    $.ajax({
        type: "POST",
        url: "Login/Login_Officials_Load",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: dataLogin,
        success: function (response) { 
            publishOfficial(response);
        },
        error: function (err) {
            publishOfficial_Error(err);
        }
    });
}

function publishOfficial(response) { 

    if (response != "") {

        document.getElementById("lblErrorOfficialLogin").innerText = response;
    }
    else {
        window.location.href = "ECIOfficials/Dashboard"
    }
    
}

function publishOfficial_Error(err) {


}
function Login_Voter_Click() { 
    var dataLogin = {
        username : document.getElementById("voterUsername").value,
        password : document.getElementById("voterPassword").value,
    };
    $.ajax({
        type: "POST",
        url: "Login/Login_Voters_Load",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: dataLogin,
        success: function (response) {
            publishVoter(response);
        },
        error: function (err) {
            publishVoter_Error(err);
        }
    });
}
function publishVoter(response) { 
    if (response != "") {

        document.getElementById("lblErrorVoterLogin").innerText = response;
    }
    else {
        window.location.href = "Voters/Overview"
    }

}
function publishVoter_Error(err) {


}