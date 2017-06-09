$(function () {
    $("#tabela input").keyup(function () {
        var index = $(this).parent().index();
        var nth = "#tabela td:nth-child(" + (index + 1).toString() + ")";
        var valor = $(this).val().toUpperCase();
        $("#tabela tbody tr").show();
        $(nth).each(function () {
            if ($(this).text().toUpperCase().indexOf(valor) < 0) {
                console.log($(this).parent());
                $(this).parent().hide();
            }
        });
    });

        //$("#tabela input").blur(function () {
        //    console.log($(this));
        //    $(this).val("");
        //});
});