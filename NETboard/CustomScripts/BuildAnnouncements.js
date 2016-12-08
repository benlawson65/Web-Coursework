$(document).ready(function () {
    $.ajax({
        //do ajax request to url below
        url: '/Announcements/BuildAnnouncements',
        

        //  if request is successful, put it inside the 
        //  AnnouncementsDiv (which is on Announcements Index.csmhtml page)
        success: function (result) {
            $('#announcementsDiv').html(result);
        }
        
    });
});