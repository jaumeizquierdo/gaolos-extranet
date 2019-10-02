/**
 * short localStorage
 */
const db = localStorage;

/**
 * short query selector
 *
 * @param      {<type>}  el      { parameter_description }
 * @return     {string}  { description_of_the_return_value }
 */
const _ = (el) => {
    return document.querySelector(el);
};
/**
 * Gets the tpl.
 *
 * @param      {<type>}  element  The element
 * @return     {string}  The tpl.
 */
const getTpl = (element) => {
    return tpl[element];
};

/**
 * Makes an editable.
 *
 * @return     {string}  { description_of_the_return_value }
 */

const makeEditable = () => {
    /*
    //CKEDITOR.inline('contentinline');
    let elements = document.querySelectorAll('.drop-element');
    let toArr = Array.prototype.slice.call(elements);
    Array.prototype.forEach.call(toArr, (obj, index) => {
        if (obj.querySelector('img')) {
            return false;
        } else {
            obj.addEventListener('click', (e) => {
                //e.preventDefault();
                //obj.children[0].setAttribute('contenteditable', '');
                //obj.focus();
                
            }); 
            obj.children[0].addEventListener('blur', (e) => {
                e.preventDefault();						
                //obj.children[0].removeAttribute('contenteditable');
            });
        }
    });
    */

};
/**
 * Removes a divs to save.
 *
 * @return     {string}  { description_of_the_return_value }
 */
const removeDivsToSave = () => {
    let elements = document.querySelectorAll('.drop-element', '.buttons');
    let toArr = Array.prototype.slice.call(elements);
    let html = '';

    Array.prototype.forEach.call(toArr, (obj, index) => {

        obj.children[0].removeAttribute('contenteditable');
        html += obj.innerHTML;
    });

    return html;
};

/**
 * Templates
 *
 * @type  string
 */

const tpl = {
    'test': '<div id="editrow"><div class="editame">Hola soy un texto que hay que editar</div><div>Editame!</div><a href="#" class="clicktest btn btn-success">Edit!</a><a href="#" class="destruyerow btn btn-danger">Elimina!</a></div>',
    'dos-columnas': '<div class="row"><div class="col-md-6"><div class="editame">Hola soy un texto que hay que editar</div></div><div class="col-md-6"><div class="editame">Hola soy un texto que hay que editar</div></div></div><div class="row buttons"><a href="#" class="clicktest btn btn-success">Edit!</a><a href="#" class="destruyerow btn btn-danger">Elimina!</a></div>',
    'tres-columnas': '<div class="row"><div class="col-md-4"><div class="editame">Hola soy un texto que hay que editar</div></div><div class="col-md-4"><div class="editame">Hola soy un texto que hay que editar</div></div><div class="col-md-4"><div class="editame">Hola soy un texto que hay que editar</div></div></div><div class="row buttons"><a href="#" class="clicktest btn btn-success">Edit!</a><a href="#" class="destruyerow btn btn-danger">Elimina!</a></div>',
    'cuatro-columnas': '<div class="row"><div class="col-md-4"><div class="editame">Hola soy un texto que hay que editar</div></div><div class="col-md-4"><div class="editame">Hola soy un texto que hay que editar</div></div><div class="col-md-4"><div class="editame">Hola soy un texto que hay que editar</div></div></div><div class="row buttons"><a href="#" class="clicktest btn btn-success">Edit!</a><a href="#" class="destruyerow btn btn-danger">Elimina!</a></div>',
    'header1': '<h1>I am header 1</h1>',
    'header2': '<h2>I am header 2</h2>',
    'header3': '<h3>I am header 3</h3>',
    'shortparagraph': '<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et</p>',
    'mediumparagraph': '<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate</p>',
    'largeparagraph': '<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a,</p>',
    'ullist': '<ul><li>item 1</li><li>item 2</li><li>item 3</li><li>item 4</li></ul>',
    'ollist': '<ol><li>item 1</li><li>item 2</li><li>item 3</li><li>item 4</li></ol>',
    'image': '<img src="http://lorempixel.com/400/200/">',
    'code': '<pre>function say(name){\n return name;\n}</pre>',
};

/**
 * init dragula
 *
 * @type  function
 */
const containers = [_('.box-left'), _('.box-right')];
const drake = dragula(containers, {
    copy(el, source) {
        return source === _('.box-left');
    },
    accepts(el, target) {
        return target !== _('.box-left');
    }
});

drake.on('out', (el, container) => {
    if (container == _('.box-right')) {
        el.innerHTML = getTpl(el.getAttribute('data-tpl'));
        el.className = 'drop-element';

        // Añadimos el attr contenteditable con valor a true, necesario para inicializar el ckeditor inline

        $('.editame').attr('contenteditable', 'true');

        // Eliminamos las instancias de ckeditor previas para añadir nuevos elementos

        for (name in CKEDITOR.instances) {
            CKEDITOR.instances[name].destroy(true);
        }

        // Buscamos todos ckeditor inline del documento con la clase editname

        var elements = CKEDITOR.document.find('.editame'), i = 0, element;
        while ((element = elements.getItem(i++))) {
            CKEDITOR.inline(element);
        }

        //makeEditable();
        //obj.children[0].setAttribute('contenteditable', '');
        //$('#editame').attr('contenteditable', 'true');

        // Inicializamos evento de edición

        $(".clicktest").click(function () {

            // Añadimos el attr contenteditable con valor a true, necesario para inicializar el ckeditor inline

            $('.editame').attr('contenteditable', 'true');

            // Eliminamos las instancias de ckeditor previas para añadir nuevos elementos

            for (name in CKEDITOR.instances) {
                CKEDITOR.instances[name].destroy(true);
            }

            // Buscamos todos ckeditor inline del documento con la clase editname

            var elements = CKEDITOR.document.find('.editame'), i = 0, element;
            while ((element = elements.getItem(i++))) {
                CKEDITOR.inline(element);
            }
        });

        $(".destruyerow").click(function () {
            el.remove();
        });

        db.setItem('savedData', _('.box-right').innerHTML);

    }
    if (container == _('.box-left')) {
        el.innerHTML = el.getAttribute('data-title');
    }
    //CKEDITOR.inline('contentinline');
});
/*
drake.on('drop', (el, container) => {
    if (container == _('.box-right')) {
        for(name in CKEDITOR.instances)
        {
            CKEDITOR.instances[name].destroy(true);
        }
    }
});
*/

/**
 * save in local storage
 */
if (typeof db.getItem('savedData') !== 'undefined') {
    _('.box-right').innerHTML = db.getItem('savedData');
    //makeEditable();
    //CKEDITOR.instances.contentprueba.destroy();
};

/**
 * reset
 */
_('.reset').addEventListener('click', (e) => {
    e.preventDefault
    if (confirm('Are you sure !')) {
        _('.box-right').innerHTML = '';
    }
    //CKEDITOR.instances.editor1.destroy();
});

/**
 * save to file
 */
_('.save').addEventListener('click', (e) => {
    e.preventDefault();
    var blob = new Blob([removeDivsToSave()], {
        type: 'text/html;charset=utf-8'
    });
    db.setItem('savedData', _('.box-right').innerHTML);
    saveAs(blob, 'file.html');
});

function setEditors(el) {
    $(".editrow").each(function (index, element) {
        CKEDITOR.inline(element, {
            toolbar: [
              { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
            ]
        });
    });
}