


function Load_Voters_Details() {

    $.ajax({
        type: "POST",
        url: "Load_Voters_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            publish_Voters_Panel(response);
        },
        error: function (err) {
            publish_Voters_Panel_Error(err);
        }
    });
}

function publish_Voters_Panel(response) { 
    var dListdatafilter = response;
    Listdatafilter = '';
    for (var i = 0; i < dListdatafilter.length; i++) {
        Listdatafilter += '<tr role="row" class="odd">';

        Listdatafilter += '<td>' + dListdatafilter[i].id + '</td>';
        Listdatafilter += '<td>' + dListdatafilter[i].firstName + '</td>';
        Listdatafilter += '<td>' + dListdatafilter[i].lastName + '</td>'; 
        Listdatafilter += '<td>' + dListdatafilter[i].address + '</td>';
        Listdatafilter += '<td>' + dListdatafilter[i].constituency + '</td>';

        if (dListdatafilter[i].isApproved == 1) {

            Listdatafilter += '<td><input type="checkbox" checked onclick="Voters_Approve(this)" id=' + dListdatafilter[i].id +'></td>'; 
        }
        else {
            Listdatafilter += '<td><input type="checkbox" onclick="Voters_Approve(this)" id=' + dListdatafilter[i].id +'></td>'; 

        }

        Listdatafilter += '</tr>';
    }
    var dd = document.getElementById('tbVoters');
    document.getElementById('tbVoters').innerHTML = Listdatafilter;
    let table = new DataTable('#tblVoters');
}

function publish_Voters_Panel_Error(err) {


} 

function Voters_Approve(approvedCheckbox) {
    var dataApproved = {
        approved: approvedCheckbox.checked,
        id: approvedCheckbox.id,
    }; 

    $.ajax({
        type: "POST",
        url: "Load_Voters_Approved_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: dataApproved,
        success: function (response) {
            publish_Voters_Panel(response);
        },
        error: function (err) {
            publish_Voters_Panel_Error(err);
        }
    });
} 