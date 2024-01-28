


function Load_Dashboard_Panel() {

    $.ajax({
        type: "POST",
        url: "Load_Dashboard_Panel",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            publish_Dashboard_Panel(response);
        },
        error: function (err) {
            publish_Dashboard_Panel_Error(err);
        }
    });
}

function publish_Dashboard_Panel(response) {


    document.getElementById("lblTotalVoters").innerText = response.totalVoters;
    document.getElementById("lblTotalConstitutions").innerText = response.totalConstitutions;
    document.getElementById("lblRegisteredParty").innerText = response.registeredParty;


}

function publish_Dashboard_Panel_Error(err) {


} 