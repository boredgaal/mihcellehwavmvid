export function initpackagemoduledraganddrop(dotnetobjref, elementId, type) {

    var __obj = {

        draggablelistmap: function (dotnetobjref, elementId) {

            this.addevents = function () {

                var jsitem = document.getElementById(elementId);
                if (jsitem != null) {

                    jsitem.addEventListener("dragstart", function (e) {

                        //var id = e.target.id;
                        //var arr = id.split("-");
                        //var packageid = arr[arr.length - 1];

                        var packageid = e.target.id;
                        if (packageid != null) {

                            var exceptDropzone = ".packagedropzone-" + packageid;
                            var dropzones = document.querySelectorAll(".packagedropzone:not(" + exceptDropzone + ")");

                            if (dropzones != null) {

                                Array.prototype.forEach.call(dropzones, function (item) {

                                    item.style.display = "block";
                                });
                            }

                            e.dataTransfer.setData("packageid", packageid);
                            e.dataTransfer.effectAllowed = "move";
                        }                        
                    });
                    jsitem.addEventListener("dragend", function (e) {

                        var dropzones = document.getElementsByClassName("packagedropzone");
                        if (dropzones != null) {

                            Array.prototype.forEach.call(dropzones, function (item) {

                                item.style.display = "none";
                                item.classList.remove("active-packagedropzone");
                            });
                        }
                    });
                }
            };
            this.removeevents = function () {

                var jsitem = document.getElementById(elementId);
                if (jsitem != null) {

                    try {
                        jsitem.removeEventListener("dragstart", (item, e) => { });
                        jsitem.removeEventListener("dragend", (item, e) => { });
                    } catch (err) { }
                }
            };
        },
        droppablelistmap: function (dotnetobjref, elementId) {

            this.addevents = function () {

                var jsitem = document.getElementById(elementId);
                if (jsitem != null) {

                    jsitem.addEventListener("dragenter", function (e) {

                        if (e.target != null)
                            e.target.classList.add("active-packagedropzone");
                    });
                    jsitem.addEventListener("dragleave", function (e) {

                        if (e.target !== null)
                            e.target.classList.remove("active-packagedropzone");
                    });
                    jsitem.addEventListener("dragover", function (e) {

                        e.preventDefault();
                        e.dataTransfer.dropEffect = "move";
                    });
                    jsitem.addEventListener("drop", function (e) {

                        e.preventDefault();

                        //var id = e.target.id;
                        //var arr = id.split("-");
                        //var droppedfieldid = arr[arr.length - 1];

                        var droppedfieldid = e.target.id;
                        var draggedfieldid = e.dataTransfer.getData("packageid");
                        dotnetobjref.invokeMethodAsync("ItemDropped", draggedfieldid, droppedfieldid);
                    });
                }
            };
            this.removeevents = function () {

                var jsitem = document.getElementById(elementId);
                if (jsitem != null) {

                    try {
                        jsitem.removeEventListener("dragenter", (item, e) => { });
                        jsitem.removeEventListener("dragleave", (item, e) => { });
                        jsitem.removeEventListener("dragover", (item, e) => { });
                        jsitem.removeEventListener("drop", (item, e) => { });
                    } catch (err) { }
                }
            };
        },
    };

    if (type === "draggable")
        return new __obj.draggablelistmap(dotnetobjref, elementId);

    if (type === "droppable")
        return new __obj.droppablelistmap(dotnetobjref, elementId);

}