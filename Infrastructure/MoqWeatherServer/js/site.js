
$(function () {
    //загрузка погоды по клику
    $(".js-weather-card__btn").click(function () {
        var thisCard = $(this).closest('.js-weather-card');
        console.log(thisCard.length);
        $.ajax({
                url: 'http://localhost:5137/api/v1/Weather/current.json',
                type: 'GET',
                dataType: 'json', 
                success: function (data) {
                    var tmpl = $.templates('#weather-card-tmpl'); 
                    var html = tmpl.render(data);
                    //$(this).closest('.js-weather-card').html(html);          
                    thisCard.html(html);          
                    //$("#js-weather-card").html(html);
                },
            error: function (xhr, status, error) { $(this).text($(this).attr("data-error")); }
            });
    });    
    /*
    var data = { "region": "Moskva", "localTime": "2026-04-07", "tempC": 7.3, "windKph": 17.6, "humidity": 57, "icon": "//cdn.weatherapi.com/weather/64x64/day/353.png" };
    var tmpl = jQuery.templates('#weather-card-tmpl');
    var html = tmpl.render(data);
    jQuery("js-weather-card").html(html);
    */
});