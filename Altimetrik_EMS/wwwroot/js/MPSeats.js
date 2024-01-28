


function Load_MPSeats_Details() {

    $.ajax({
        type: "POST",
        url: "Load_MPSeats_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            publish_MPSeats_Panel(response);
        },
        error: function (err) {
            publish_MPSeats_Panel_Error(err);
        }
    });
}

function publish_MPSeats_Panel(response) {
    var dListdatafilter = response;
    Listdatafilter = '';
    for (var i = 0; i < dListdatafilter.length; i++) {
        Listdatafilter += '<tr role="row" class="odd">';

        Listdatafilter += '<td>' + dListdatafilter[i].id + '</td>';
        Listdatafilter += '<td>' + dListdatafilter[i].name + '</td>';
        Listdatafilter += '<td>' + dListdatafilter[i].state + '</td>';
        Listdatafilter += '<td>' + dListdatafilter[i].areaCode + '</td>';

        Listdatafilter += '</tr>';
    }
    var dd = document.getElementById('tbMPSeats');
    document.getElementById('tbMPSeats').innerHTML = Listdatafilter;
    let table = new DataTable('#tblMPSeats');
}

function publish_MPSeats_Panel_Error(err) {


}

function MPSeats_Add(approvedCheckbox) {
    var dataConstituency = {
        name: document.getElementById("txtConstituency").value,
        state: document.getElementById("ddlConstituency").value,
        areacode: document.getElementById("txtAreaCode").value,
    };

    $.ajax({
        type: "POST",
        url: "Load_MPSeat_Add_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: dataConstituency,
        success: function (response) {
            publish_MPSeat_Add_Panel(response);
        },
        error: function (err) {
            publish_MPSeat_Add_Panel_Error(err);
        }
    });
} 


function publish_MPSeat_Add_Panel(response) {
    location.reload();
}

function publish_MPSeat_Add_Panel_Error(err) {


}