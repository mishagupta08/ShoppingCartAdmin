$(document).ready(function () {
    $(".preloader").hide();

    /**Edit functions**/

    $("a[name=editButton").unbind();
    $("a[name=editButton").bind("click", function () {
        var editId = $(this).attr("data-id");
        var viewName = $("#viewName").val();
        OpenEditView(editId, viewName);
    });

    $('#categoryList').unbind();
    $('#categoryList').change(function (e) {
        GetSubCategoryByCategory();
    });

    /**Edit functions*End**/

    /**Delete functions**/
    $("a[name=deleteButton").unbind();
    $("a[name=deleteButton").bind("click", function () {
        var editId = $(this).attr("data-id");
        $("#deleteId").attr("data-delete-id", editId);
        $("#deleteBtn").click();
    });

    $("#deleteYesBtn").unbind();
    $("#deleteYesBtn").bind("click", function () {
        var viewName = $("#viewName").val();
        DeleteView($("#deleteId").attr("data-delete-id"), viewName);
    });
    
    if (document.getElementById('files') != null) {
        document.getElementById('files').addEventListener('change', handleFileSelect, false);
    }
    /**Delete functions*end**/

    /**Image Button Functions**/

    $("a[name=imageButton").unbind();
    $("a[name=imageButton").bind("click", function () {
        var editId = $(this).attr("data-id");
        var viewName = $("#imageViewName").val();
        OpenImageView(editId, viewName);
    });

    /**Image Button Functions**/

    $("form#addProductImage").submit(function (e) {
        e.preventDefault();
        var formData = new FormData(this);
        var ProdId = $("#ProdId").val();
        $.ajax({
            url: '/Dashboard/UploadProductImages',
            type: 'Post',
            cache: false,
            contentType: false,
            processData: false,
            data: formData, //your string data
            //data: formData
        }).done(function (result) {
            $("#urlHidden").html("");
            var hiddenHtml = ""

            //$.each(result, function (key, value) {
            //    //$("#urlHidden").append($("<option></option>").val(value.Id).html(value.Name));
            //    hiddenHtml += "<input type='hidden' value=" + value + " name='imageUrls' />";
            //});

            $.ajax({
                url: '/Dashboard/AddProductImageUrls',
                type: 'Post',
                datatype: 'Json',
                data: { "prodId": ProdId, "imageUrls": result }
            }).done(function (result) {
                if (result == "") {
                    $("#status").html("Images added successfully.");
                }
                else {
                    $("#status").html(result);
                }
            }).fail(function (error) {
                $("#status").html(error.statusText);
                $(".preloader").hide();
            });

            $("#urlHidden").html(hiddenHtml);
            $(".preloader").hide();
        });

        //$.ajax({
        //    url: "/Dashboard/UploadProductImages",
        //    type: 'POST',
        //    cache: false,
        //    contentType: false,
        //    processData: false,
        //    data: formData,
        //}).done(function (result) {
        //    $("#urlHidden").html("");
        //    var hiddenHtml = ""
        //    $.each(result, function (key, value) {
        //        //$("#urlHidden").append($("<option></option>").val(value.Id).html(value.Name));
        //        hiddenHtml += "<input type='hidden' value=" + value + " name='imageUrls' />";
        //    });
        //    $("#urlHidden").html(hiddenHtml);
        //});
    });
});

function GetSubCategoryByCategory() {
    var id = $("#categoryList").val();
    $(".preloader").show();
    $.ajax({
        url: '/Dashboard/GetSubCategoryByCategory',
        type: 'Post',
        datatype: 'Json',
        data: { Id: id }
    }).done(function (result) {
        if (result == null || result == undefined || result == "") {
            $("#error").html(result);
        }
        else {

            $("#subcategoryList").html("");
            $.each(result, function (key, value) {
                $("#subcategoryList").append($("<option></option>").val(value.Id).html(value.Name));
            });
        }

        $(".preloader").hide();
    });
}

function DeleteView(id, view) {
    $(".preloader").show();
    $.ajax({
        url: 'DeleteView',
        type: 'Post',
        datatype: 'Json',
        data: { "editId": id, "viewName": view }
    }).done(function (result) {
        $(".close").click();
        if (result == "") {
            $("#deleteError").html(result);
        }
        else {
            location.reload();
        }
        location.reload();
        $(".preloader").hide();
    }).fail(function (error) {
        $("#error").html(error.statusText);
        $(".preloader").hide();
    });

    return false;
}

function DeleteImage(id) {
    $(".preloader").show();
    $.ajax({
        url: 'DeleteProductImage',
        type: 'Post',
        datatype: 'Json',
        data: { "editId": id }
    }).done(function (result) {
        $("#deleteCloseImageBtn").click();
        if (result == "") {            
            $("#" + id).remove()
            $("#deleteError").html(result);
        }
        else {
            location.reload();
        }

        location.reload();
        $(".preloader").hide();
    }).fail(function (error) {
        $("#error").html(error.statusText);
        $(".preloader").hide();
    });

    return false;
}

function OpenEditView(id, view) {
    $(".preloader").show();
    $.ajax({
        url: 'GetEditView',
        type: 'Post',
        datatype: 'Json',
        data: { "editId": id, "viewName": view }
    }).done(function (result) {
        $("#addViewcontainer").html(result);
        $("#modelBtn").click();

        $(".preloader").hide();
    }).fail(function (error) {
        $("#error").html(error.statusText);
        $(".preloader").hide();
    });

    return false;
}

function OpenImageView(id, view) {
    $(".preloader").show();
    $.ajax({
        url: 'GetImageView',
        type: 'Post',
        datatype: 'Json',
        data: { "editId": id, "viewName": view }
    }).done(function (result) {
        $("#addImageViewcontainer").html(result);
        $("img[name=closeImage").unbind();
        $("img[name=closeImage").bind("click", function () {
            var editId = $(this).attr("data-id");
            $("#deleteId").attr("data-delete-id", editId);
            $("#deleteImageBtn").click();
        });

        $("#deleteImageYesBtn").unbind();
        $("#deleteImageYesBtn").bind("click", function () {
            DeleteImage($("#deleteId").attr("data-delete-id"));
        });

        $("#imageBtn").click();
        $(".preloader").hide();
    }).fail(function (error) {
        $("#error").html(error.statusText);
        $(".preloader").hide();
    });

    return false;
}

function AddAdminUser() {
    var detail = $('#adduser').serialize();
    $(".preloader").show();
    $.ajax({
        url: 'AddAdminUser',
        type: 'Post',
        datatype: 'Json',
        data: detail
    }).done(function (result) {
        if (result == "") {
            var returnUrl = $("#returnUrl").val();
            if (returnUrl != null && returnUrl != "" && returnUrl != undefined) {
                window.location.href = returnUrl;
            }
            else {
                //window.location.href = 'Dashboard/Index';
                $(".close").click();
                location.reload();
            }
        }
        else {
            $("#error").html(result);
            $(".preloader").hide();
        }
    }).fail(function (error) {
        $("#error").html(error.statusText);
        $(".preloader").hide();
    });

    return false;
}

function AddGroup() {
    var detail = $('#addGroup').serialize();
    $(".preloader").show();
    $.ajax({
        url: 'AddGroup',
        type: 'Post',
        datatype: 'Json',
        data: detail
    }).done(function (result) {
        if (result == "") {
            var returnUrl = $("#returnUrl").val();
            if (returnUrl != null && returnUrl != "" && returnUrl != undefined) {
                window.location.href = returnUrl;
            }
            else {
                //window.location.href = 'Dashboard/Index';
                $(".close").click();
                location.reload();
            }
        }
        else {
            $("#error").html(result);
            $(".preloader").hide();
        }
    }).fail(function (error) {
        $("#error").html(error.statusText);
        $(".preloader").hide();
    });

    return false;
}

function AddState() {
    var detail = $('#addState').serialize();
    $(".preloader").show();
    $.ajax({
        url: 'AddState',
        type: 'Post',
        datatype: 'Json',
        data: detail
    }).done(function (result) {
        if (result == "") {
            var returnUrl = $("#returnUrl").val();
            if (returnUrl != null && returnUrl != "" && returnUrl != undefined) {
                window.location.href = returnUrl;
            }
            else {
                //window.location.href = 'Dashboard/Index';
                $(".close").click();
                location.reload();
            }
        }
        else {
            $("#error").html(result);
            $(".preloader").hide();
        }
    }).fail(function (error) {
        $("#error").html(error.statusText);
        $(".preloader").hide();
    });

    return false;
}

function AddBrand() {
    var detail = $('#addBrand').serialize();
    $(".preloader").show();
    $.ajax({
        url: 'AddBrand',
        type: 'Post',
        datatype: 'Json',
        data: detail
    }).done(function (result) {
        if (result == "") {
            var returnUrl = $("#returnUrl").val();
            if (returnUrl != null && returnUrl != "" && returnUrl != undefined) {
                window.location.href = returnUrl;
            }
            else {
                //window.location.href = 'Dashboard/Index';
                $(".close").click();
                location.reload();
            }
        }
        else {
            $("#error").html(result);
            $(".preloader").hide();
        }
    }).fail(function (error) {
        $("#error").html(error.statusText);
        $(".preloader").hide();
    });

    return false;
}

function AddCategory() {
    var detail = $('#addCategory').serialize();
    $(".preloader").show();
    $.ajax({
        url: 'AddCategory',
        type: 'Post',
        datatype: 'Json',
        data: detail
    }).done(function (result) {
        if (result == "") {
            var returnUrl = $("#returnUrl").val();
            if (returnUrl != null && returnUrl != "" && returnUrl != undefined) {
                window.location.href = returnUrl;
            }
            else {
                //window.location.href = 'Dashboard/Index';
                $(".close").click();
                location.reload();
            }
        }
        else {
            $("#error").html(result);
            $(".preloader").hide();
        }
    }).fail(function (error) {
        $("#error").html(error.statusText);
        $(".preloader").hide();
    });

    return false;
}

function AddSubCategory() {
    var detail = $('#addSubCategory').serialize();
    $(".preloader").show();
    $.ajax({
        url: 'AddSubCategory',
        type: 'Post',
        datatype: 'Json',
        data: detail
    }).done(function (result) {
        if (result == "") {
            var returnUrl = $("#returnUrl").val();
            if (returnUrl != null && returnUrl != "" && returnUrl != undefined) {
                window.location.href = returnUrl;
            }
            else {
                //window.location.href = 'Dashboard/Index';
                $(".close").click();
                location.reload();
            }
        }
        else {
            $("#error").html(result);
            $(".preloader").hide();
        }
    }).fail(function (error) {
        $("#error").html(error.statusText);
        $(".preloader").hide();
    });

    return false;
}

function AddProductImage() {
    var n = path.lastIndexOf('\\');
    var selectedFileName = path.substring(n + 1);
    $.ajax({
        url: 'AddImage',
        dataType: 'json',
        type: 'POST',
        data: { data: reader.result, fileName: selectedFileName }, //your string data
    }).done(function (result) {
        //$("#imageFilePath").html(response);
        document.getElementById("imageFilePath").value = result;
        if (viewName.indexOf("Product") >= 0) {
            AddProduct();
        }
        else if (viewName.indexOf("Offer") >= 0) {
            AddOfferDetail();
        }
        else if (viewName.indexOf("Category") >= 0) {
            AddCategory();
        }
        else {
            AddBannerDetail();
        }
    });
}

function AddImage() {
    $(".preloader").show();
    var path = document.getElementById('files').value;
    var viewName = $('#viewName').val();
    if (path == "") {
        if (viewName.indexOf("Product") >= 0) {
            AddProduct();
        }
        else if (viewName.indexOf("Offer") >= 0) {
            AddOfferDetail();
        }
        else if (viewName.indexOf("Category") >= 0) {
            AddCategory();
        }
        else {
            AddBannerDetail();
        }
    }
    else {
        var n = path.lastIndexOf('\\');
        var selectedFileName = path.substring(n + 1);
        $.ajax({
            url: 'AddImage',
            dataType: 'json',
            type: 'POST',
            data: { data: reader.result, fileName: selectedFileName }, //your string data
        }).done(function (result) {
            //$("#imageFilePath").html(response);
            document.getElementById("imageFilePath").value = result;
            if (viewName.indexOf("Product") >= 0) {
                AddProduct();
            }
            else if (viewName.indexOf("Offer") >= 0) {
                AddOfferDetail();
            }
            else if (viewName.indexOf("Category") >= 0) {
                AddCategory();
            }
            else {
                AddBannerDetail();
            }
        });
    }
}

function AddBannerDetail() {
    var formData = $('#addBanner').serialize();
    $.ajax({
        url: "AddBanner",
        type: "post",
        data: formData,
    }).done(function (result) {
        if (result == "") {
            var returnUrl = $("#returnUrl").val();
            if (returnUrl != null && returnUrl != "" && returnUrl != undefined) {
                window.location.href = returnUrl;
            }
            else {
                //window.location.href = 'Dashboard/Index';
                $(".close").click();
                location.reload();
            }
        }
        else {
            $("#error").html(result);
            $(".preloader").hide();
        }

    }).fail(function (error) {
        $("#error").html(error.statusText);
        $(".preloader").hide();
    });
}

function AddOfferDetail() {
    var formData = $('#addOffer').serialize();
    $.ajax({
        url: "AddOfferImage",
        type: "post",
        data: formData,
    }).done(function (result) {
        if (result == "") {
            var returnUrl = $("#returnUrl").val();
            if (returnUrl != null && returnUrl != "" && returnUrl != undefined) {
                window.location.href = returnUrl;
            }
            else {
                //window.location.href = 'Dashboard/Index';
                $(".close").click();
                location.reload();
            }
        }
        else {
            $("#error").html(result);
            $(".preloader").hide();
        }

    }).fail(function (error) {
        $("#error").html(error.statusText);
        $(".preloader").hide();
    });
}

function AddProduct() {
    var formData = $('#addProduct').serialize();
    $.ajax({
        url: "AddProduct",
        type: "post",
        data: formData,
    }).done(function (result) {
        if (result == "") {
            var returnUrl = $("#returnUrl").val();
            if (returnUrl != null && returnUrl != "" && returnUrl != undefined) {
                window.location.href = returnUrl;
            }
            else {
                //window.location.href = 'Dashboard/Index';
                $(".close").click();
                location.reload();
            }
        }
        else {
            $("#error").html(result);
            $(".preloader").hide();
        }

    }).fail(function (error) {
        $("#error").html(error.statusText);
        $(".preloader").hide();
    });
}

/*******start upload image*********/

var reader;
var progress = document.querySelector('.percent');

function abortRead() {
    reader.abort();
}

function errorHandler(evt) {
    switch (evt.target.error.code) {
        case evt.target.error.NOT_FOUND_ERR:
            alert('File Not Found!');
            break;
        case evt.target.error.NOT_READABLE_ERR:
            alert('File is not readable');
            break;
        case evt.target.error.ABORT_ERR:
            break; // noop
        default:
            alert('An error occurred reading this file.');
    };
}

function updateProgress(evt) {
    // evt is an ProgressEvent.
    if (evt.lengthComputable) {
        var percentLoaded = Math.round((evt.loaded / evt.total) * 100);
        // Increase the progress bar length.
        if (percentLoaded < 100) {
            progress.style.width = percentLoaded + '%';
            progress.textContent = percentLoaded + '%';
        }
    }
}

function handleFileSelect(evt) {
    // Reset progress indicator on new file selection.
    progress.style.width = '0%';
    progress.textContent = '0%';

    reader = new FileReader();
    reader.onerror = errorHandler;
    reader.onprogress = updateProgress;
    reader.onabort = function (e) {
        alert('File read cancelled');
    };
    reader.onloadstart = function (e) {
        document.getElementById('progress_bar').className = 'loading';
    };
    reader.onload = function (e) {
        // Ensure that the progress bar displays 100% at the end.
        progress.style.width = '100%';
        progress.textContent = '100%';
        //setTimeout("document.getElementById('progress_bar').className='';", 2000);
    }

    // Read in the image file as a binary string.
    //reader.readAsArrayBuffer(evt.target.files[0]);

    reader.readAsDataURL(evt.target.files[0]);
}

/***********End upload image*************/