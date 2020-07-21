﻿Vue.component('vue-multiselect', window.VueMultiselect.default);

let app = new Vue({
    components: {
        Multiselect: window.VueMultiselect.default
    },
    el: "#app",
    data: {
        gameId: document.getElementById('gameId').value,
        model: null,
        genres: [],
        isLoadingGenres: false,
        selectedGenres: [],
        genresmultiselect: [],
    },
    methods: {
        genreSearch: function (filter) {
            axios.get(`/GameSource/Genre/?filter=${encodeURIComponent(filter)}`)
                .then(response => {
                    this.genresmultiselect = response.data;
                })
                .catch(error => {
                    console.log("Error: " + error.statusText);
                });
        }
    }
});

