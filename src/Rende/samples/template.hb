<div class="entry">
  <h1>{{title}}</h1>
  <div class="body">
    {{body}}
  </div>

<ul id="products">
    {{#each products}}
        <li data-id="{{id}}">{{name}}</li>
    {{/each}}
  </ul>
</div>