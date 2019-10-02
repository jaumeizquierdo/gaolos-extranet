// GAOLOS 2017 v1.0
// File: Cms.js
// Desc: Eventos para contentbuilder y función de grabar el contenido

///////////////////////////////////////////////////////////////

// ace editor #CustomComponentsTextArea

var textareaComponentesJs = $('#CustomComponentsTextArea');

var editorComponentesJS = ace.edit("CustomComponentsTextArea");
editorComponentesJS.setTheme("ace/theme/monokai");
editorComponentesJS.getSession().setMode("ace/mode/html");
editorComponentesJS.getValue();

editorComponentesJS.getSession().on('change', function () {
    textareaComponentesJs.val(editorComponentesJS.getSession().getValue());
});

textareaComponentesJs.val(editorComponentesJS.getSession().getValue());

///////////////////////////////////////////////////////////////