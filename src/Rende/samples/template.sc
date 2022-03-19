<ul id='products'>
  {{- for product in products }}
    <li>
      <h2>{{ product.name }}</h2>
      <span>{{ product.price }}</span>
      <p>{{ product.description | string.truncate 15 }}</p>
    </li>
  {{ end }}
</ul>