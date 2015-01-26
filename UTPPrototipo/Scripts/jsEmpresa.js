function modificarDatosUsuario(id) {
    //var url = "/Empresa/_AdministrarUsuarioEditar/" + id;
    //debugger;
    var url = '@Url.Action("_AdministrarUsuarioEditar", "Empresa")' + "/" + id;

    $.get(url, function (data) {
        $('#divUsuarioContenedorEditarTMP').html(data);
        $.validator.unobtrusive.parse('#divUsuarioContenedorEditarTMP');
        $('#divModalUsuarioEditarTMP').modal('show');
    });
}

function cerrarModalTMP() {

    //debugger;
    $('.modal.in').modal('hide');
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
   
    
}