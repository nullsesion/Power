
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

                //встраиваем отмену запроса
                $(thisCard).find(".js-weather-card__loading").click(function () {
                    if (jqXHR && typeof jqXHR.abort === 'function') {
                        jqXHR.abort();
                        alert('Запрос отменен');
                        thisCard.removeClass('loading');
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
                thisCard.removeClass('loading').addClass('reload');
            }
            });
    });
    $(".js-weather-card__btn_forecast").click(function () {
        var jqXHR = $.ajax({
            url: jQuery("html").attr("api-url").trim() + '/api/v1/Weather/forecast.json',
            type: 'GET',
            dataType: 'json',
            beforeSend: function () {
                //добавить возможность отмены
            },
            success: function (data) {
                var tmpl = $.templates('#weather-card-tmpl');
                $(".js-load-forecast_target").find(".col").each(function (i, e) {
                    var tmpl = $.templates('#weather-card-tmpl');
                    var html = tmpl.render(data[i]);
                    $(e).find(".js-weather-card__into").html(html)
                    $(".js-load-forecast_target .js-weather-card").removeClass('loading').addClass('reload');
                });
            },
            error: function (xhr, status, error) {
                //добавить ошибку
            }
        });
        
    });
});