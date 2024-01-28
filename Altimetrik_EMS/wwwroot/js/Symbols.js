


function Load_Symbols_Details() {

    $.ajax({
        type: "POST",
        url: "Load_Symbols_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            publish_Symbols_Panel(response);
        },
        error: function (err) {
            publish_Symbols_Panel_Error(err);
        }
    });
}

function publish_Symbols_Panel(response) {
    var dListdatafilter = response;
    Listdatafilter = '';
    for (var i = 0; i < dListdatafilter.length; i++) {
        Listdatafilter += '<tr role="row" class="odd">';

        Listdatafilter += '<td>' + dListdatafilter[i].id + '</td>';
        Listdatafilter += '<td>' + dListdatafilter[i].name + '</td>'; 
        if (dListdatafilter[i].symbol != null) {

            Listdatafilter += '<td><img id="ItemPreview" style="width: 30px;" src="data:image/png;base64,' + dListdatafilter[i].symbol + '"></td>'; 
        }
        else {

            Listdatafilter += '<td></td>';
        }

        Listdatafilter += '</tr>';
    }
    var dd = document.getElementById('tbSymbols'); 
    document.getElementById('tbSymbols').innerHTML = Listdatafilter;
    let table = new DataTable('#tblSymbols');
}

function publish_Symbols_Panel_Error(err) {


}

function Symbols_Add(approvedCheckbox) {
    
    var formData = new FormData();
    formData.append("name", $("#txtName").val());
    formData.append("photo", $("#formFile")[0].files[0]);

    $.ajax({
        url: "Load_Symbols_Add_Panel",
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            publish_Symbols_Add_Panel(response);
        },
        error: function (error) {
            publish_Symbols_Add_Panel_Error(err);
        }
    });
} 

function publish_Symbols_Add_Panel(response) {
    location.reload();
}

function publish_Symbols_Add_Panel_Error(err) {


}




function Load_Symbols_Party_DropDown_Details() {

    $.ajax({
        type: "POST",
        url: "Load_Symbols_Party_DropDown_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            Symbols_Party_DropDown_Load(response);
        },
        error: function (err) {
             
        }
    });
}
function Symbols_Party_DropDown_Load(response) {
    
    var ddlParty = document.getElementById("ddlParty"); 

    for (var i = 0; i < response.length; i++) {

        var addoption = document.createElement('option');
        var single = response[i];
        addoption.text = single.partyName;
        addoption.value = single.id; 

        ddlParty.add(addoption, i); 

    } 
}

function Load_Symbols_Constituency_DropDown_Details() {

    $.ajax({
        type: "POST",
        url: "Load_Symbols_Constituency_DropDown_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            Symbols_Constituency_DropDown_Load(response);
        },
        error: function (err) {

        }
    });
}
function Symbols_Constituency_DropDown_Load(response) {

    var ddlConstituency = document.getElementById("ddlConstituency");

    for (var i = 0; i < response.length; i++) {

        var addoption = document.createElement('option');
        var single = response[i];
        addoption.text = single.name;
        addoption.value = single.id;

        ddlConstituency.add(addoption, i);
    }
}