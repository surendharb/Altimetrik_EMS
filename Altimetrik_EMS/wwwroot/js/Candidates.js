


function Load_Candidates_Details() {

    $.ajax({
        type: "POST",
        url: "Load_Candidates_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            publish_Candidates_Panel(response);
        },
        error: function (err) {
            publish_Candidates_Panel_Error(err);
        }
    });
}

function publish_Candidates_Panel(response) {
    var dListdatafilter = response;
    Listdatafilter = '';
    for (var i = 0; i < dListdatafilter.length; i++) {
        Listdatafilter += '<tr role="row" class="odd">';

        Listdatafilter += '<td>' + dListdatafilter[i].id + '</td>';
        Listdatafilter += '<td>' + dListdatafilter[i].name + '</td>';
        Listdatafilter += '<td>' + dListdatafilter[i].address + '</td>';
        Listdatafilter += '<td>' + dListdatafilter[i].party + '</td>';
        Listdatafilter += '<td>' + dListdatafilter[i].constituency + '</td>';

        Listdatafilter += '</tr>';
    }
    var dd = document.getElementById('tbCandidates'); 
    document.getElementById('tbCandidates').innerHTML = Listdatafilter;
    let table = new DataTable('#tblCandidates');
}

function publish_Candidates_Panel_Error(err) {


}

function Candidates_Add(approvedCheckbox) {
    var dataConstituency = {
        name: document.getElementById("txtName").value,
        address: document.getElementById("txtAddress").value,
        partyID: document.getElementById("ddlParty").value,
        constituencyID: document.getElementById("ddlConstituency").value,
    };

    $.ajax({
        type: "POST",
        url: "Load_Candidates_Add_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: dataConstituency,
        success: function (response) {
            publish_Candidates_Add_Panel(response);
        },
        error: function (err) {
            publish_Candidates_Add_Panel_Error(err);
        }
    });
} 

function publish_Candidates_Add_Panel(response) {
    location.reload();
}

function publish_Candidates_Add_Panel_Error(err) {


}




function Load_Candidates_Party_DropDown_Details() {

    $.ajax({
        type: "POST",
        url: "Load_Candidates_Party_DropDown_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            Candidates_Party_DropDown_Load(response);
        },
        error: function (err) {
             
        }
    });
}
function Candidates_Party_DropDown_Load(response) {
    
    var ddlParty = document.getElementById("ddlParty"); 

    for (var i = 0; i < response.length; i++) {

        var addoption = document.createElement('option');
        var single = response[i];
        addoption.text = single.partyName;
        addoption.value = single.id; 

        ddlParty.add(addoption, i); 

    } 
}

function Load_Candidates_Constituency_DropDown_Details() {

    $.ajax({
        type: "POST",
        url: "Load_Candidates_Constituency_DropDown_Panel",
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