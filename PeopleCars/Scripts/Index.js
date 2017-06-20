new Vue({
    el: '#app',
    mounted: function () {
        this.loadPeople();
    },
    data: {
        people: [],
        modal: {},
        car:{},
        cars:[],
        isAddPersonModel: true,
        carshasvalue:false,
        personId:''
    },
    methods: {
        addClick: function () {
            this.isAddPersonModel = true;
            this.modal = {};
            $(".modal").modal();
        },
        savePerson: function () {
            $.post('/home/addperson', this.modal, () => {
                $(".modal").modal('hide');
                this.loadPeople();
            });

        },
        addCarClick: function (p) {
            this.isAddPersonModel = false;
            this.personId = p;
            this.car = {};
            $(".modal").modal();
        },

        saveCar: function () {
            $.post('/home/addCar', { car: this.car, personId: this.personId }, () => {
                $(".modal").modal('hide');
                this.loadPeople();
            });
        },
        
        showCars: function (p) {
            $.get('/home/getCars', { personId: p }, cars=> {
                this.cars = cars;
                if (this.cars.length > 0) {
                    this.carshasvalue = true;
                }
            });
        },

        loadPeople: function () {
            $.get('/home/getpeople', people => {
                this.people = people;
            });
        },
    }
})