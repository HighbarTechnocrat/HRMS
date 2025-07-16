$(function() {
  $('.txtsearch, select').on('change', function(event) {
    var $element = $(event.target),
      $container = $element.closest('.example');
    if (!$element.data('tagsinput'))
      return;
    var val = $element.val();
    if (val === null)
      val = "null";
    var data = ($.isArray(val) ? JSON.stringify(val) : "\"" + val.replace('"', '\\"') + "\"");
    $("#MainContent_hdvalue").val(data.split("\"").join(""));
    //$("#MainContent_hdarray").val(JSON.stringify($element.tagsinput('items')));
  }).trigger('change');
    //<script>
  var cities = new Bloodhound({
      datumTokenizer: Bloodhound.tokenizers.obj.whitespace('Name'),
      queryTokenizer: Bloodhound.tokenizers.whitespace,
      prefetch: 'data/users.json'
  });
  cities.initialize();
  var elt = $('.txtsearch');
  elt.tagsinput({
      tagClass: function (item) {
          switch (item.continent) {
              case '1': return 'label label-primary';
              case '2': return 'label label-danger label-important';
              case '3': return 'label label-success';
              case '4': return 'label label-default';
              case '5': return 'label label-warning';
          }
      },
      itemValue: 'Email',
      itemText: 'Name',
      typeaheadjs: {
          name: 'users',
          displayKey: 'Name',
          source: cities.ttAdapter()
      }
  });
  //elt.tagsinput('add', { "value": 1, "text": "Amsterdam", "continent": "1" });
  //elt.tagsinput('add', { "value": 4, "text": "Washington", "continent": "2" });
  //elt.tagsinput('add', { "value": 7, "text": "Sydney", "continent": "3" });
  //elt.tagsinput('add', { "value": 10, "text": "Beijing", "continent": "4" });
  //elt.tagsinput('add', { "value": 13, "text": "Cairo", "continent": "5" });
    //</script>
});
