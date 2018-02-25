(function () {
    'use strict';

    var serviceId = 'datacontext';
    angular.module('app').factory(serviceId, datacontext);
    datacontext.$inject = ['homedataservice',
        'categorydataservice',
        'productdataservice',
        'invintorydataservice',
        'invintoryproductdataservice',
        'orderdataservice',
        'orderdetailsdataservice','statisticsdataservice']
    function datacontext(homedataservice,
        categorydataservice,
        productdataservice,
        invintorydataservice,
        invintoryproductdataservice,
        orderdataservice,
        orderdetailsdataservice, statisticsdataservice) {


        var service = {

            homedataservice: homedataservice,
            categorydataservice: categorydataservice,
            productdataservice: productdataservice,
            invintorydataservice: invintorydataservice,
            invintoryproductdataservice: invintoryproductdataservice,
            orderdataservice: orderdataservice,
            orderdetailsdataservice: orderdetailsdataservice,
            statisticsdataservice: statisticsdataservice

        };

        return service;



    }
})();