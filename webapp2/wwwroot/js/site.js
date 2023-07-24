// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    // Get the current date and time in ISO format (local timezone)
    var currentDateTime = new Date().toISOString().slice(0, 16);

    // Set the minimum attribute of all datetime-local input fields to the current date and time
    $("input[type='datetime-local']").attr("max", currentDateTime);
});