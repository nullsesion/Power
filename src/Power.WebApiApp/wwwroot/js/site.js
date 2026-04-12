
$(function () {
    //загрузка погоды по клику
    $(".js-weather-card__btn").click(function () {
        var thisCard = $(this).closest('.js-weather-card');
        var jqXHR = $.ajax({
            url: jQuery("html").attr("api-url").trim() + '/api/v1/Weather/current.json',
            type: 'GET',
            dataType: 'json', 
            beforeSend: function () {
                thisCard.addClass('loading');
                thisCard.removeClass('has-error');

                //встраиваем отмену запроса
                $(thisCard).find(".js-weather-card__loading").click(function () {
                    if (jqXHR && typeof jqXHR.abort === 'function') {
                        jqXHR.abort();
                        alert('Запрос отменен');
                        thisCard.removeClass('loading').removeClass('has-error');
                        thisCard.addClass('reload');
                    }
                });
            },
            success: function (data) {
                var tmpl = $.templates('#weather-card-tmpl'); 
                var html = tmpl.render(data);
                    
                thisCard.find(".js-weather-card__load").html(html);
                thisCard.addClass('load').removeClass('loading').removeClass('reload');
            },
            error: function (xhr, status, error) {
                thisCard.removeClass('loading').addClass('reload').addClass('has-error');
                var returnRequest = thisCard.find(".js-weather-card__btn").attr("data-error");
                thisCard.find(".js-weather-card__btn").text(returnRequest);
            }
            });
    });
    $(".js-weather-card__btn_forecast").click(function () {
        var thisForecastTarget = $(this).closest(".js-wrapp");
        thisForecastTarget.find(".js-load-forecast_target").show();

        var jqXHR = $.ajax({
            url: jQuery("html").attr("api-url").trim() + '/api/v1/Weather/forecast.json',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var tmpl = $.templates('#weather-card-tmpl');
                thisForecastTarget.find(".js-load-forecast_target").find(".col").each(function (i, e) {
                    var tmpl = $.templates('#weather-card-tmpl');
                    var html = tmpl.render(data[i]);
                    $(e).find(".js-weather-card__into").html(html)
                    thisForecastTarget.find(".js-load-forecast_target .js-weather-card").removeClass('loading').addClass('reload');
                    $("js-wrapp").find(".js-load-forecast_target .js-weather-card").removeClass('loading').addClass('reload')
                    thisForecastTarget.find(".js-weather-card__btn_forecast").hide();
                });
            },
        });
        
    });

    $.ajax({
        url: jQuery("html").attr("api-url").trim() + '/api/v1/Weather/hours.json',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $(".js-weather-hour").html("");
            var stringTable = '<table>';
            $(data).each(function (i, e) {
                stringTable += '<tr><td>' + e.time + ":</td><td>" + e.tempC + 'C&deg; </td></tr>'
            });
            stringTable += '</table>';
            $(".js-weather-hour").append(stringTable);
        }
    });
    
});