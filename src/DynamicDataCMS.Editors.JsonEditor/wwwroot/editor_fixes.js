﻿function editorChange() {
    var tabs = document.getElementsByClassName("tab-pane");
    if (tabs && tabs.length > 0) {
        tabs[0].classList.add("active");
    }
}

addEventListener('DOMContentLoaded', () => {
    if (!window.editor)
        return;

    editorChange();
    window.editor.on('ready', editorChange);
});

//hide broken images
(function () {
    var allimgs = document.images;
    for (var i = 0; i < allimgs.length; i++) {
        allimgs[i].onerror = function () {
            this.style.visibility = "hidden"; // Other elements aren't affected.
        }
        allimgs[i].onload = function () {
            this.style.visibility = ""; // Other elements aren't affected.
        }
    }
})();


function jsonLoaded() {
    if (!window.editor)
        return;

    showProperties(window.editor.schema.properties, window.editor.editors.root);
}

//show properties that are in the schema but not in the json data
function showProperties(schema, editor)
{
    for (var propertyName in schema) {
        editor.addObjectProperty(propertyName);

        if (schema[propertyName].properties)
            showProperties(schema[propertyName].properties, editor.editors[propertyName]);

        //you can get the value like this: myObject[propertyName]
    }
}