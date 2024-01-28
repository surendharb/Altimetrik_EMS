


function Load_Result_Details() {
    var dataConstituency = {
        id: document.getElementById("ddlConstituency").value,
    };

    $.ajax({
        type: "POST",
        url: "Load_Result_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: dataConstituency,
        success: function (response) {
            publish_Result_Panel(response);
        },
        error: function (err) {
            publish_Result_Panel_Error(err);
        }
    });
}

function publish_Result_Panel(response) {
    var dListdatafilter = response;
    Listdatafilter = '';
    for (var i = 0; i < dListdatafilter.length; i++) {
        Listdatafilter += '<tr role="row" class="odd">';

        Listdatafilter += '<td>' + dListdatafilter[i].candidateName + '</td>';
        Listdatafilter += '<td>' + dListdatafilter[i].partyName + '</td>';
        Listdatafilter += '<td></td>';
        Listdatafilter += '<td>' + dListdatafilter[i].totalVotes + '</td>';
        if (dListdatafilter[i].status == null) {
            Listdatafilter += '<td>Lost</td>';
        }
        else {
            Listdatafilter += '<td>' + dListdatafilter[i].status + '</td>';
        }
        Listdatafilter += '</tr>';
    }
    document.getElementById('tbResult').innerHTML = Listdatafilter;
    let table = new DataTable('#tb1Result');
}
function publish_Result_Panel_Error(err) {


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

    Load_Result_Details();
}