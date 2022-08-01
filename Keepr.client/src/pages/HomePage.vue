<template>
  <div class="masonry-frame m-3">
    <div class="m-2" v-for="k in keeps" :key="k.id">
      <Keep :keep="k" />
    </div>
  </div>

</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'

export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      await keepsService.getKeeps()
    })

    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
.masonry-frame {
  columns: 4;
}

div {
  break-inside: avoid;
  width: auto;
  height: auto;
}
</style>
