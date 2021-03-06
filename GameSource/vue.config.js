﻿module.exports = {
    outputDir: 'wwwroot/vue',
    filenameHashing: false,
    configureWebpack: {
        optimization: {
            splitChunks: false
        },
        resolve: {
            alias: {
                'vue$': 'vue/dist/vue.esm.js'
            }
        }
    },
}