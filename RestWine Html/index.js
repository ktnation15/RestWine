const url = 'https://restwine20240609133143.azurewebsites.net/api/wine'

Vue.createApp({
    data() {
        return {
            Wines: [],
            allwnines: [],
            deleteId: 0,
            deleteMessage: '',
            idToGetBy: -1,
            singleWine: null,
            addData: { "id": 0, "manufacturer": "", "year": 0, "price": 0, "rating": 0 },
            updateData: { "id": 0, "manufacturer": "", "year": 0, "price": 0, "rating": 0 },
            addMessage: '',
            updateMessage: '',
            manufacturerFilter: '',
        }
    },
    methods: {
        async getAllWines() {
            try {
                const response = await axios.get(url);
                this.Wines = response.data;
                this.allwnines = this.Wines;
            } catch (ex) {
                alert(ex.message);
            }
        },
        async getWineById() {
            const urlWithId = url + '/' + this.idToGetBy;
            try {
                const response = await axios.get(urlWithId);
                this.singleWine = response.data;
            } catch (ex) {
                alert(ex.message);
            }
        },
        async deleteWine() {
            const urlWithId = url + '/' + this.deleteId;
            try {
                const response = await axios.delete(urlWithId);
                this.deleteMessage = response.status + " " + response.statusText;
                this.getAllWines();
            } catch (ex) {
                alert(ex.message);
            }
        },
        async addWine() {
            try {
                const response = await axios.post(url, this.addData);
                this.addMessage = "response " + response.status + " " + response.statusText;
                this.getAllWines();
                this.addData = { "id": 0, "manufacturer": "", "year": 0, "price": 0, "rating": 0 };
            } catch (ex) {
                alert(ex.message);
            }
        },
        async updateWine() {
            const urlWithId = url + '/' + this.updateData.id;
            try {
                const response = await axios.put(urlWithId, this.updateData);
                this.updateMessage = "response " + response.status + " " + response.statusText;
                this.getAllWines();
                this.updateData = { "id": 0, "manufacturer": "", "year": 0, "price": 0, "rating": 0 };
            } catch (ex) {
                alert(ex.message);
            }
        },
        sortByRating() {
            this.Wines.sort((a, b) => a.rating - b.rating);
            console.log(this.Wines);
        },
        sortByPrice() {
            this.Wines.sort((a, b) => a.price - b.price);
            console.log(this.Wines);
        },
        sortByYear() {
            this.Wines.sort((a, b) => a.year - b.year);
            console.log(this.Wines);
        },
        filterByManufacturer() {
            this.Wines = this.allwnines.filter(wine => wine.manufacturer.toLowerCase().startsWith(this.manufacturerFilter.toLowerCase()));
        },
    },
    created() {
        this.getAllWines();
    }
}).mount('#app');