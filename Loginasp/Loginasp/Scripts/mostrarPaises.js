    $(document).ready(function () {

    var selects = "";
    $(paises).each(function (indice, elemento) {
        selects += "<option value=" + elemento.name + ">" + elemento.name + "</option>"

    });

    $("#comboPais").html(selects);
});