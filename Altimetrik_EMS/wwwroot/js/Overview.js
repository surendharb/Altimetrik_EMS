


function Load_Voters_Overview_Profile_Details() {

    $.ajax({
        type: "POST",
        url: "Load_Voters_Overview_Profile_Details",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            publish_Voters_Overview_Profile_Panel(response);
        },
        error: function (err) {
            publish_Voters_Overview_Profile_Panel_Error(err);
        }
    });
}

function publish_Voters_Overview_Profile_Panel(response) {
    document.getElementById("addFirstName").value = response.firstName;
    document.getElementById("addLastName").value = response.lastName;
    document.getElementById("addUsername").value = response.username;
    document.getElementById("addAddress").value = response.address;
}

function publish_Voters_Overview_Profile_Panel_Error(err) {


}


function Load_Voters_Overview_Pool_Check_Details() {

    $.ajax({
        type: "POST",
        url: "Load_Voters_Overview_Poll_Check_Details",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            publish_Voters_Overview_Poll_Check_Panel(response);
        },
        error: function (err) {
            publish_Voters_Overview_Poll_Check_Panel_Error(err);
        }
    });
}

function publish_Voters_Overview_Poll_Check_Panel(response) {

}

function publish_Voters_Overview_Poll_Check_Panel_Error(err) {


}

function Load_Voters_Overview_Pool_Details() {

    $.ajax({
        type: "POST",
        url: "Load_Voters_Overview_Pooling_Details",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            publish_Voters_Overview_Pooling_Panel(response);
        },
        error: function (err) {
            publish_Voters_Overview_Pooling_Panel_Error(err);
        }
    });
}

function publish_Voters_Overview_Pooling_Panel(response) {

    document.getElementById("spanConstituency").innerText = response.constituencyName;

    var dListdatafilter = response;
    var Listdatafilter = "";
    Listdatafilter += '<br/>';

    if (response.canVote == false) {


        Listdatafilter += '<div class="alert alert-success alert-dismissible fade show" role="alert">';
        Listdatafilter += '<h4 class="alert-heading">You have completed the voting</h4>';

        Listdatafilter += '<hr>';
        Listdatafilter += '<p class="mb-0">Thank you for you vote.</p>';
        Listdatafilter += '</div>';

        document.getElementById('rbPooling').innerHTML = Listdatafilter;


        document.getElementById('btnCreate').disabled = true;
        document.getElementById('btnCreate').style.opacity = (0.4);

        return;
    }

    for (var i = 0; i < dListdatafilter.poolingParties.length; i++) {

        Listdatafilter += '<div class="form-check">';

        Listdatafilter += '<td><img id="ItemPreview" style="width: 30px;" src="data:image/png;base64,' + dListdatafilter.poolingParties[i].partySymbol + '"></td>';

        Listdatafilter += '<input class="form-check-input" type="radio" name="gridRadios" id="' + dListdatafilter.poolingParties[i].candidateID + '" value="' + dListdatafilter.poolingParties[i].candidateID + '">';
        Listdatafilter += '<label class="form-check-label" for="gridRadios1">';
        Listdatafilter += ' - ' + dListdatafilter.poolingParties[i].candidateName + '(' + dListdatafilter.poolingParties[i].partyName + ')';
        Listdatafilter += '</label>';
        Listdatafilter += '</div>';
        Listdatafilter += '<br/>';

    }

    var dd = document.getElementById('rbPooling');

    document.getElementById('rbPooling').innerHTML = Listdatafilter;

}

function publish_Voters_Overview_Pooling_Panel_Error(err) {


}


function Pooling_Add() {

    var ele = document.getElementsByName('gridRadios');
    var selectedCandidate = 0;
    for (i = 0; i < ele.length; i++) {
        if (ele[i].checked)
            selectedCandidate = ele[i].value;
    }
    debugger;
    var dataVoting = {
        candidateID: selectedCandidate,
    };

    dataVoting
    $.ajax({
        type: "POST",
        url: "Add_Voters_Overview_Pooling_Details",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: dataVoting,
        success: function (response) {
            publish_Add_Voters_Overview_Pooling_Panel(response);
        },
        error: function (err) {
            publish_Add_Voters_Overview_Pooling_Panel_Error(err);
        }
    });
}

function publish_Add_Voters_Overview_Pooling_Panel(response) {


}

function publish_Add_Voters_Overview_Pooling_Panel_Error(err) {


} 