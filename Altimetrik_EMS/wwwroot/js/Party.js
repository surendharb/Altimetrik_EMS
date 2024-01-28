


function Load_Party_Details() {

    $.ajax({
        type: "POST",
        url: "Load_Party_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            publish_Party_Panel(response);
        },
        error: function (err) {
            publish_Party_Panel_Error(err);
        }
    });
}

function publish_Party_Panel(response) {
    var dListdatafilter = response;
    Listdatafilter = '';
    for (var i = 0; i < dListdatafilter.length; i++) {
        Listdatafilter += '<tr role="row" class="odd">';

        Listdatafilter += '<td>' + dListdatafilter[i].id + '</td>';
        Listdatafilter += '<td>' + dListdatafilter[i].partyName + '</td>';
        Listdatafilter += '<td>' + dListdatafilter[i].shortName + '</td>'; 
        if (dListdatafilter[i].currentSymbol != null) {

            Listdatafilter += '<td><img id="ItemPreview" style="width: 30px;" src="data:image/png;base64,' + dListdatafilter[i].currentSymbol + '"></td>';
        }
        else {

            Listdatafilter += '<td></td>';
        }

        Listdatafilter += '</tr>';
    }
    var dd = document.getElementById('tbParty'); 
    document.getElementById('tbParty').innerHTML = Listdatafilter;
    let table = new DataTable('#tblParty');
}

function publish_Party_Panel_Error(err) {


}

function Party_Add() {
    var dataParty = {
        partyname: document.getElementById("txtName").value,
        shortname: document.getElementById("txtShortName").value, 
    };

    $.ajax({
        type: "POST",
        url: "Load_Party_Add_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: dataParty,
        success: function (response) {
            publish_Party_Add_Panel(response);
        },
        error: function (err) {
            publish_Party_Add_Panel_Error(err);
        }
    });
} 

function publish_Party_Add_Panel(response) {
    location.reload();
}

function publish_Party_Add_Panel_Error(err) {


}

