// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//const { data } = require("jquery");

// Write your JavaScript code.
$(function () {
    console.log("page is ready");
   // $('#myAlert').hide();
   // $(document).on("click", "#modaleditbutton", function () {
       
      //  console.log("Edit button was clicked " + $(this).val());
        // store product Id
        //var productId = $(this).val();
        //$.ajax({
        //    dataType: 'Json',
        //    method: 'POST',
        //    url: "/Product/Edit",
        //    data: {
        //        "Id": productId
        //    },
        //    success: function(data){
        //        console.log(data)
        //        $('modal-dialog').html(data);
        //        //fill in the input fields in the modal
        //        //$("#modal-input-id").val(data.id);
        //        //$("#modal-input-name").val(data.name);
        //        //$("#modal-input-price").val(data.price);
        //        //$("#modal-input-description").val(data.description);
        //       $('#editModal').modal('show');
        //    }

                                

        //});
   // });
    //$(document).on("click", "#modaldeletebutton", function () {

    //    console.log("Delete button was clicked " + $(this).val());
    //    // store product Id
    //    if (window.confirm("Confirm Delete")) {
    //        var productId = $(this).val();
    //        $.ajax({
    //            dataType: 'Json',
    //            method: 'POST',
    //            url: "/Product/Delete",
    //            data: {
    //                "Id": productId
    //            },
    //            success: function (data) {
    //               // console.log(data + "Record Deleted")

    //                //const toastLive = document.getElementById('deleteToast')  
    //                //const toast = new bootstrap.Toast(toastLive, {
    //                //    animation: true,
    //                //    autohide: true,
    //                //    delay: 3000
    //                //})
    //                //toast.show()
    //               // window.location.href = "/Product/Index"
    //                //window.onload = (event) => {
    //                //    const toastLive = document.getElementById('deleteToast')

    //                //    const toast = new bootstrap.Toast(toastLive, {
    //                //        animation: true,
    //                //        autohide: true,
    //                //        delay: 3000
    //                //    })
    //                //    toast.show();
    //                //}
    //            }

    //        });
    //    }
    //    else {

    //        //do nothing
    //    }
    //});
    $(document).on("click", "#showdetailsmodal", function () {  
        
        var productId = $(this).val();
        ShowProductDetails(productId);
       
    });
    $(document).on("click", "#showdetailsmodal1", function () {

        var productId = $(this).val();
        ShowProductDetails(productId);

    });
    $(document).on("click", ".product-title", function () {

        var productId = $(this).attr('data');
        ShowProductDetails(productId);

    });
    $(document).on("click", ".card-img-top", function () {

       // var itemImage = $(this).attr('src');
        var productId = $(this).attr('alt');
        ShowProductDetails(productId);
        
    });
    $(document).on("mouseover", ".card-img-top", function () {

        
        $(this).animate({
            opacity: '0.5'
            
        })
    });
    $(document).on("mouseleave", ".card-img-top", function () {


        $(this).animate({
            opacity: '1'

        })
    });

    $(document).on('click', '#save-button', function () {
        
            //get values from input field and create object of to send to ProcessEdit
        var product = {

            "Id": $("#modal-input-id").val(),
            "Name": $("#modal-input-name").val(),
            "Price": $("#modal-input-price").val(),
            "Description": $("#modal-input-description").val()
          };
     
        $.ajax({

            dataType: 'html',
            method:'POST',
            url: "/Product/ProcessEditReturnPartial",
            data: product,
            success: function (data) {
                console.log(data);
                $("#card-number-" + product.Id).html(data);
                //Alert worked fine aswell but preffered toast
                //$('#myAlert').show();
                //window.setTimeout(function () {
                //    $("#myAlert").hide();
                //}, 2000);  
                
                //$('#liveToast').show();
                //window.alert("Item has been updated.")
               // const toastTrigger = document.getElementById('liveToastBtn')
                const toastLive = document.getElementById('updateToast')
                //if (toastTrigger) {
                   // toastTrigger.addEventListener('click', () => {
                // need to pass Toast arguments because delay stuck to 500 by default for some reason I have checked everywhere but can't seems to fix it 
                //this one works for now.'
                const toast = new bootstrap.Toast(toastLive, {
                    animation: true,
                    autohide: true,
                    delay: 3000
                    })
                toast.show()

                   // })
               // }
            },
            error: function (err) {
                    console.log(err.statusText)
            }
        });
    })
    //$(document).on('click','#createnew', function () {

    //            const toastLive = document.getElementById('insertToast')

    //            const toast = new bootstrap.Toast(toastLive, {
    //                animation: true,
    //                autohide: true,
    //                delay: 3000
    //            })
    //            toast.show()
        //    },
        //    error: function (err) {
        //        console.log(err.statusText)
        //    }

        //});
    /*})*/
    //window.onload = (event) => {
    //    const toastLive = document.getElementById('deleteToast')

    //    const toast = new bootstrap.Toast(toastLive, {
    //        animation: true,
    //        autohide: true,
    //        delay: 3000
    //    })
    //    toast.show();
    //}
    $('#deleteModal').on('show.bs.modal', function (e) {
        var yourparameter = e.relatedTarget.dataset.myparameter;
        $('#delete-button').attr("href", "/Product/Delete/" + yourparameter);
        console.log(yourparameter);
         //Do some stuff w/ it.
    });
    $(document).on('click', '#addToBag', function () {

        var product = {

            "Id": $('#detailitemid').html(),
            "Name": $('#detailitemname').html(),
            "Price": $('#detailitemprice').html(),
            "Description": $('#detailitemdescription').html()
        };
        $.ajax({

            dataType: 'html',
            method: 'POST',
            url: "/Product/AddToCart",
            data: product,
            success: function (data) {
                console.log(data);
                $(".badge").html(data);
               
                const toastLive = document.getElementById('itemaddedtobag');
                
                const toast = new bootstrap.Toast(toastLive, {
                    animation: true,
                    autohide: true,
                    delay: 3000
                })
                toast.show()
            },
            error: function (err) {
                console.log(err.statusText)
            }
        });
           

    });
    //$('#deleteModal').on('show.bs.modal', function (e) {
    //    $(this).find('.delete-button').attr('href', $(e.relatedTarget).data('href'));
    //});
    //$(document).on("click", "#deletebutton", function () {

    //    var productId = $(this).val();
    //    var itemImage = $('#image-' + productId).attr('src');
    //    $.ajax({
    //        dataType: 'Json',
    //        method: 'POST',
    //        url: '/Product/ShowDetails',
    //        data: {
    //            "Id": productId
    //        },
    //        success: function (data) {
    //            console.log(data)
    //            $('#detailitemid1').html(data.id);
    //            $('#detailitemname1').html(data.name);
    //            $('#detailitemprice1').html(data.price);
    //            $('#detailitemdescription1').html(data.description);
    //            $('#detailitemimage1').html("<img class=" + "card-img-top" + " " + "src=" + itemImage + "/>");
    //            $('#delete-button').attr("value",  productId);

    //        },
    //        error: function (err) {
    //            console.log(err.statusText)
    //        }

    //    });
    //})
    //$("#delete-button").on('click', function () {

    //    //get values from input field and create object of to send to ProcessEdit
    //    var product = $('#delete-button').val();

    //    $.ajax({

    //        dataType: 'Json',
    //        method: 'POST',
    //        url: "/Product/Delete",
    //        data: {
    //            "Id": product
    //        },
    //        success: function (data) {
    //            console.log(data);

    //            //const toastLive = document.getElementById('deleteToast');

    //            //const toast = new bootstrap.Toast(toastLive, {
    //            //    animation: true,
    //            //    autohide: true,
    //            //    delay: 3000
    //            //})
    //            //toast.show()

    //            // })
    //            // }
    //        },
    //        error: function (err) {
    //            console.log(err.statusText)
    //        }
    //    });
    //})
    $(document).on("click", "#modaleditbutton", function () {

        console.log("Edit button was clicked " + $(this).val());
        // store product Id
        var productId = $(this).val();
        $.ajax({
            // dataType: 'Json',
            //method: 'POST',
            url: "/Product/Edit",
            data: {
                "Id": productId
            },
            success: function (data) {
                console.log(data)
                $('#edit-modal-dialog').html(data);
                //fill in the input fields in the modal
                //$("#modal-input-id").val(data.id);
                //$("#modal-input-name").val(data.name);
                //$("#modal-input-price").val(data.price);
                //$("#modal-input-description").val(data.description);
                $('#editModal').modal('show')
            }



        });
    });
   
    $(document).on('click', '#sidebarCollapse', function () {
            $('#sidebar').toggleClass('active');
    });

    
    //$('#editModal').on('hide.bs.modal', function (e) {
    //    console.log("edit model is closed")
    function ShowProductDetails(productId) {
        var itemImage = $('#image-' + productId).attr('src');
        $.ajax({
            dataType: 'Json',
            method: 'POST',
            url: '/Product/ShowDetails',
            data: {
                "Id": productId
            },
            success: function (data) {
                console.log(data)
                $('#detailitemid').html(data.id);
                $('#detailitemname').html(data.name);
                $('#detailitemprice').html(data.price);
                $('#detailitemdescription').html(data.description);
                /* $('#detailitemimage').html("<img class=" + "card-img-top" + " " + "src=" + itemImage + "/>");*/
                 $('#detailitemimage').html("<img class="+ "card-img-top" + " src=" + itemImage + "/>");

            },
            error: function (err) {
                console.log(err.statusText)
            }

        }); 
    }
});




   


    
