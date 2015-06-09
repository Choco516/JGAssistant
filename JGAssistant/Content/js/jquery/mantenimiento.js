$(document).ready(function () {

    $("#nuevocont").click(function () {
        $("#divnuevocont").show();
        $("#divtablacont").hide();
    });

    $("#guardarcont").click(function () {
        $("#divtablacont").show();
        $("#divnuevocont").hide();
    });

    $("#guardarnav").click(function () {
        $("#divtablacont").show();
        $(".blockInput").prop("disabled", true);
        $('#pais1').prop('disabled', true).trigger("liszt:updated");
        $("#guardarnav").hide();
    });
});