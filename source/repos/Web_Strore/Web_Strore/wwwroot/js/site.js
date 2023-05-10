// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const { error } = require("../_Adminassets/vendors/jquery/external/sizzle/dist/sizzle");

// Write your JavaScript code.

$(document).ready(function (){
    $("#search").autocomplete({
        minLenght: 0,
        source: function (request, response) {

            $.ajax({
                url: 'AdminProducts/Search',
                data: { "search": request.term },
                dataType: "json",
                method: "POST",
                success: function (res) {
                    console.log(res);
                        $('#Details').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }

            });
        },
        select: function (e, i) {
            $("#listSearch").vali(i.item.id);
        }
    });
});