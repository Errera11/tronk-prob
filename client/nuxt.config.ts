import tailwindcss from '@tailwindcss/vite'

export default defineNuxtConfig({
  compatibilityDate: '2025-07-15',
  devtools: { enabled: true },
  modules: ['dayjs-nuxt', '@nuxt/ui', '@bootstrap-vue-next/nuxt', '@nuxtjs/i18n', '@vueuse/nuxt', '@pinia/nuxt'],
  css: ['~/assets/main.css', 'bootstrap/dist/css/bootstrap.min.css'],
  vite: {
    plugins: [tailwindcss()],
  },
  srcDir: './src',
  runtimeConfig: {
    public: {
      apiBase: '',
    },
  },

  imports: {
    dirs: [
      'composables',
      'composables/**',
    ],
  },
})
