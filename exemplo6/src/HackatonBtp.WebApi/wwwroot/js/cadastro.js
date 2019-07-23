//CLASSES

var TimeIntegrante = class TimeIntegrante {
    constructor(
        nome,
        dataNascimento,
        telefone,
        rg,
        curso,
        universidade,
        deficiencia,
        linkedin,
        git,
        experiencia,
        categoria,
        comunidadeDev,
        email
    ) {
        this.nome = nome;
        this.rg = rg;
        this.dataNascimento = dataNascimento;
        this.telefone = telefone;
        this.universidade = universidade;
        this.curso = curso;
        this.posuiDeficiencia = deficiencia !== "" ? true : false;
        this.descricaoDeficiencia = deficiencia;
        this.linkedin = linkedin;
        this.git = git;
        this.experiencia = experiencia;
        this.categoria = categoria;
        this.comunidadeDev = comunidadeDev;
        this.email = email;
    }
}

var Time = class Time {
    constructor(nomeTime, emailTime, integrantes) {
        this.nome = nomeTime;
        this.email = emailTime;
        this.integrantes = integrantes;
    }
}

//GLOBALS

//var BASE_URL_REQUEST = "https://hackathonbtp.azurewebsites.net/btp/";
var BASE_URL_REQUEST = "http://localhost:80/btp/";
var TimeIntegrantes = new Array();

var NomeIntegrante = "";
var NomeTime = "";
var EmailTime = "";

//CONSTS TIME
var SLIDE_EMAIL_TIME = 3;
var SLIDE_INFO_INTEGRANTES = 4;
var SLIDE_CONFIRMACAO_TIME = 8;
var SLIDE_OUTRO_INTEGRANTE = 7;
var SLIDE_CONFIRMAR_TIME = 10;

//CONSTS SOZINHO
var SLIDE_EMAIL_SOZINHO = 12;
var SLIDE_INFORMACOES = 13;
var SLIDE_CONFIRMAR_INTEGRANTE = 17;

//CADASTRO TIME

$("#btnEscolhaTime").click(function () {
    var nomeTime = $("#nomeTime").val();
    if (nomeTime == "") {
        $("#ErroNomeTime").text("Preencha o nome do time");
        $("#nomeTime").focus();
        return false;
    } else {
        $("#ErroNomeTime").text("");
    }


    $.getJSON(BASE_URL_REQUEST + "time&VerificarNomeTime=" + nomeTime, function (
        data
    ) {
        if (Object.keys(data)[0] == "error") {
            $("#nomeTime").focus();
            $("#ErroNomeTime").text("Já exite um time com o mesmo nome. Por favor, escolha outro.");
        } else if (Object.keys(data)[0] == "success") {
            NomeTime = nomeTime;
            $("#Erro").text("");
            $("#carouselCadastro").carousel(SLIDE_EMAIL_TIME); //Avança para o slide de email
        }
    });
});
$("#btnEmailTime").click(function () {
    var emailTime = $("#emailTime").val();
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    if (!filter.test(emailTime)) {

        $("#ErroMailTime").text("O email não está em um formato válido");
        $("#emailTime").focus();
        return false;
    }
    else {
        $("#ErroMailTime").text("");
    }

    $.getJSON(BASE_URL_REQUEST + "time&VerificarEmailTime=" + emailTime, function (
        data
    ) {
        if (Object.keys(data)[0] == "error") {
            $("#ErroMailTime").text("O email informado já está em uso");

        } else if (Object.keys(data)[0] == "success") {
            EmailTime = emailTime;

            $("#carouselCadastro").carousel(SLIDE_INFO_INTEGRANTES);
        }
    });
});

$("#btnProximoIntegrante").click(function () {

    var nome = $("#nomeITime").val();
    var dataNascimento = $("#dataNascimentoITime").val();
    var telefone = $("#telefoneITime").val();
    var rg = $("#rgITime").val();
    var curso = $("#cursoITime").val();
    var universidade = $("#universidadeITime").val();
    var deficiencia = $("#deficienciaITime").val();


    var data = new Date(dataNascimento);

    var dataAtual = new Date(Date.now());


    if (nome.trim().length < 3 || nome.length > 150) {
        $("#nomeITime").focus();
        $("#erroCadastroTime").text("Preencha o nome do integrante");
        return false;
    }
    else {
        $("#erroCadastroTime").text("");
    }
    if (data.getTime() >= dataAtual.getTime()) {
        $("#erroCadastroTime").text("Por favor, preencha com um data válida");
        $("#dataNascimentoITime").focus();
        return false;
    }

    if (dataNascimento == '') {
        $("#dataNascimentoITime").focus();
        $("#erroCadastroTime").text("Preencha a data de nascimento");
        return false;
    }
    else {
        $("#erroCadastroTime").text("");
    }
    if (telefone.length < 10) {
        $("#erroCadastroTime").text("Preencha corretamente o telefone");
        $("#telefoneITime").focus();
        return false;
    }
    else {
        $("#erroCadastroTime").text("");
    }

    if (rg.trim().length < 8) {
        $("#rgITime").focus();
        $("#erroCadastroTime").text("Preencha corretamente o RG");
        return false;
    }
    else {
        $("#erroCadastroTime").text("");
    }
    if (universidade.trim() != "" && curso.trim() == "" || curso.trim() == null) {

        $("#cursoITime").focus();
        $("#erroCadastroTime").text("Por favor, preencha o campo universidade");
        return false;

    }
    else {
        $("#erroCadastroTime").text("");
    }
    if (curso.trim() != "" && universidade.trim() == "" || universidade.trim() == null) {

        $("#cursoITime").focus();
        $("#erroCadastroTime").text("Por favor, preencha o campo curso");
        return false;

    }
    else {
        $("#erroCadastroTime").text("");
    }



});

$("#btnFinalCadastroIntegrante2").click(function () {

    var nome = $("#nomeITime").val();
    var dataNascimento = $("#dataNascimentoITime").val();
    var telefone = $("#telefoneITime").val();
    var rg = $("#rgITime").val();
    var curso = $("#cursoITime").val();
    var universidade = $("#universidadeITime").val();
    var deficiencia = $("#deficienciaITime").val();
    var linkedin = $("#linkedin").val();
    var git = $("#git").val();
    var experiencia = $("#experiencia").val();
    var comunidadeDev = $("#comunidade").val();
    var email = $("#emailCandidato").val();

    var categoria = "";

    if ($('#dev').prop('checked')) {

        categoria = "Dev";

    } else if ($('#design').prop('checked')) {

        categoria = "Design";

    } else {
        categoria = "Biz Dev";
    }

    var data = new Date(dataNascimento);

    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    if (!filter.test(email)) {

        $("#ErroMailIntegrate").text("Por favor, preencha o email do participante");
        $("#ErroMailIntegrate").focus();
        return false;
    }
    else {
        $("#ErroMailIntegrate").text("");
    }

    var timeIntegrante = new TimeIntegrante(
        nome,
        data,
        telefone,
        rg,
        curso,
        universidade,
        deficiencia,
        linkedin,
        git,
        experiencia,
        categoria,
        comunidadeDev,
        email
    );

    console.log(timeIntegrante);

    TimeIntegrantes.push(timeIntegrante);

    if (TimeIntegrantes.length == 4) {
        $("#carouselCadastro").carousel(SLIDE_CONFIRMACAO_TIME); //Slide de confirmação
    } else {
        $("#carouselCadastro").carousel(SLIDE_OUTRO_INTEGRANTE);
    }

});

$("#btnFinalizarTime").click(function () {
    $("#spanTime").text(NomeTime);
    $("#spanEmailTime").text(EmailTime);

    $.each(TimeIntegrantes, function (index, value) {

        $("#spanIntegrantesTime").append(value.nome);

        if (TimeIntegrantes[TimeIntegrantes.length - 1] !== value) {
            $("#spanIntegrantesTime").append(", ");
        }
    });
});

$("#btnConfirmarTime").click(function () {

    var checado = $('#confirmaregulamento:checkbox:checked').length > 0

    if (!checado) {
        $("#erroCheck").text("Você precisa aceitar os termos para participar!");

        return false;
    }
    else {

        $("#erroCheck").text("");

        var time = new Time(NomeTime, EmailTime, TimeIntegrantes);

        $('body').loadingModal({
            position: 'auto',
            text: 'Aguarde... Estamos finalizando sua inscrição...',
            color: '#fff',
            opacity: '0.7',
            backgroundColor: 'rgb(0,0,0)',
            animation: 'doubleBounce'
        });

        $.ajax({
            type: "POST",
            url: BASE_URL_REQUEST + "time",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(time),

            statusCode: {
                201: handle201Time,
                400: handle400Time,
            },
        });

    }

});

//CADASTRO SOZINHO

$("#btnNomeTimeSozinho").click(function () {
    var nomeTime = $("#nomeTimeSozinho").val();

    if (nomeTime == "") {
        $("#Erro").text("Por favor, preencha o nome do time");
        $("#nomeTimeSozinho").focus();
        return false;
    }
    else {
        $("#Erro").text("");
    }
    $.getJSON(BASE_URL_REQUEST + "time&VerificarNomeTime=" + nomeTime, function (
        data
    ) {
        if (Object.keys(data)[0] == "error") {
            $("#nomeTimeSozinho").focus();
            $("#Erro").text("O nome do time já está em uso. Por favor, escolha outro");
        } else if (Object.keys(data)[0] == "success") {
            NomeTime = nomeTime;
            $("#Erro").text("");
            $("#carouselCadastro").carousel(SLIDE_EMAIL_SOZINHO);
        }
    });
});

$("#btnEmailSozinho").click(function () {
    var emailTime = $("#nomeEmailSoznho").val();
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    if (!filter.test(emailTime)) {
        $("#ErroMail").text("Por favor, preencha com um email válido");
        $("#nomeEmailSoznho").focus();
        return false;
    }
    else {
        $("#ErroMail").text("");
    }

    $.getJSON(BASE_URL_REQUEST + "time&VerificarEmailTime=" + emailTime, function (
        data
    ) {
        if (Object.keys(data)[0] == "error") {
            $("#ErroMail").text("O email informado já está em uso");
        } else if (Object.keys(data)[0] == "success") {
            EmailTime = emailTime;

            $("#carouselCadastro").carousel(SLIDE_INFORMACOES);
        }
    });
});

$("#btnCadastroIntegrante").click(function () {
    NomeIntegrante = $("#nomeIntegranteSo").val();

    $("#timeSozinho").text(NomeTime);
    $("#emailSozinho").text(EmailTime);
    $("#NomeSozinho").text(NomeIntegrante);

    var telefone = $("#telefone1").val();
    var rg = $("#rgSo").val();
    var curso = $("#cursoSo").val();
    var universidade = $("#universidadeSo").val();
    var deficiencia = $("#deficienciaSo").val();
    var dataNascimento = $("#datanascimento1").val();

    var data = new Date(dataNascimento);
    var dataAtual = new Date(Date.now());

    if (NomeIntegrante.trim().length < 3 || NomeIntegrante.trim().length > 150) {


        $("#erroCadastro").text("Por favor, preencha seu nome");
        $("#NomeSozinho").focus();
        return false;
    }
    else {
        $("#erroCadastro").text("");
    }

    if (data.getTime() >= dataAtual.getTime()) {
        $("#erroCadastro").text("Por favor, preencha com um data válida");
        $("#datanascimento1").focus();
        return false;
    }

    if (dataNascimento == '') {

        $("#erroCadastro").text("Por favor, preencha sua data de nascimento");
        $("#datanascimento1").focus();
        return false;
    }
    else {
        $("#erroCadastro").text("");
    }
    if (telefone.length < 10) {
        $("#erroCadastro").text("Por favor, preencher o telefone corretamente");
        $("#telefone1").focus();
        return false;
    }
    else {
        $("#erroCadastro").text("");
    }

    if (rg.trim().length < 8) {

        $("#erroCadastro").text("Por favor, preencha corretamente o RG");
        $("#NomeSozinho").focus();
        return false;
    }
    else {
        $("#erroCadastro").text("");
    }
    if (curso != "" && universidade == "" || universidade == null) {
        $("#erroCadastro").text("Por favor, preencha o campo universidade");
        $("#cursoSo").focus();
        return false;

    }
    else {
        $("#erroCadastro").text("");
    }

    if (universidade.trim() != "" && curso.trim() == "" || curso.trim() == null) {
        $("#erroCadastro").text("Por favor, preencha o campo curso");
        $("#cursoSo").focus();
        return false;

    }
    else {
        $("#erroCadastro").text("");
    }
});

$("#btnFinalCadastroIntegrante").click(function () {

    NomeIntegrante = $("#nomeIntegranteSo").val();

    $("#timeSozinho").text(NomeTime);
    $("#emailSozinho").text(EmailTime);
    $("#NomeSozinho").text(NomeIntegrante);

    var telefone = $("#telefone1").val();
    var rg = $("#rgSo").val();
    var curso = $("#cursoSo").val();
    var universidade = $("#universidadeSo").val();
    var deficiencia = $("#deficienciaSo").val();
    var dataNascimento = $("#datanascimento1").val();
    var linkedin = $("#linkedin2").val();
    var git = $("#git2").val();
    var experiencia = $("#experiencia2").val();
    var comunidadeDev = $("#comunidade2").val();

    var data = new Date(dataNascimento);

    var categoria = "";

    if ($('#dev2').prop('checked')) {

        categoria = "Dev";

    } else if ($('#design2').prop('checked')) {

        categoria = "Design";

    } else {
        categoria = "Biz Dev";
    }

    var timeIntegrante = new TimeIntegrante(
        NomeIntegrante,
        data,
        telefone,
        rg,
        curso,
        universidade,
        deficiencia,
        linkedin,
        git,
        experiencia,
        categoria,
        comunidadeDev,
        "hackathonbtp@gr.com.br"
    );

    TimeIntegrantes.push(timeIntegrante);

});

$("#btnConfirmarIntegrante").click(function () {

    var checado = $('#confirmaregulamento2:checkbox:checked').length > 0;

    if (!checado) {
        $("#erroCheckRegulamento").text("Você precisa aceitar os termos para participar!");

        return false;
    }
    else {

        $("#erroCheck").text("");

        var time = new Time(NomeTime, EmailTime, TimeIntegrantes);

        $('body').loadingModal({
            position: 'auto',
            text: 'Aguarde... Estamos finalizando sua inscrição...',
            color: '#fff',
            opacity: '0.7',
            backgroundColor: 'rgb(0,0,0)',
            animation: 'doubleBounce'
        });

        $.ajax({
            type: "POST",
            url: BASE_URL_REQUEST + "time",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(time),

            statusCode: {
                201: handle201Integrante,
                400: handle400Integrante
            }
        });
    }
});

var handle201Integrante = function () {

    $('body').loadingModal('hide');
    $('body').loadingModal('destroy');

    $("#carouselCadastro").carousel(SLIDE_CONFIRMAR_INTEGRANTE);
    $("#nomeTImeConfirmacao").text(NomeTime);
};

var handle400Integrante = function () {

    $('body').loadingModal('hide');
    $('body').loadingModal('destroy');

    $("#carouselCadastro").carousel(SLIDE_CONFIRMAR_INTEGRANTE);

    $("#parabensIntegrante").text("");
    $("#nomeTImeConfirmacao").text(NomeTime + ",");

    $("#infoCadastroIntegrante").text("");

    $("#infoCadastroIntegrante").text(
        "Ocorreu um erro ao realizar sua incrição. Por favor, tente novamente."
    );
};

var handle201Time = function () {
    $('body').loadingModal('hide');
    $('body').loadingModal('destroy');

    $("#carouselCadastro").carousel(SLIDE_CONFIRMAR_TIME);
    $("#spanSucessoTime").text(NomeTime);
};
var handle400Time = function () {

    $('body').loadingModal('hide');
    $('body').loadingModal('destroy');

    $("#carouselCadastro").carousel(SLIDE_CONFIRMAR_TIME);
    $("#spanSucessoTime").text(NomeTime + ",");

    $("#parabensTime").text("");
    $("#infoCadastroTime").text("");

    $("#infoCadastroTime").text(
        "Ocorreu um erro ao realizar sua incrição. Por favor, tente novamente."
    );
};
$(document).ready(function () {
    $('#telefoneITime').mask('(00) 0000-00009');
    $('#telefone1').mask('(00) 0000-00009');
});


$("#btnAdicionarOutro").click(function () {
    $("#nomeITime").val("");
    $("#dataNascimentoITime").val("");
    $("#telefoneITime").val("");
    $("#rgITime").val("");
    $("#cursoITime").val("");
    $("#universidadeITime").val("");
    $("#deficienciaITime").val("");

});