$(function () {
  var $doc = $(document);
  var $win = $(window);
  var click = "ontouchend" in document ? 'tap' : 'click';

  /* 绑定单击事件  */
  $doc.on(click, '[ht-click]', function() {
    var $that = $(this),
    custom = $that.attr('ht-click').split(',');
    var fcustom = function () {
      if (custom.length) {
        $doc.trigger(custom.shift(),[$that,fcustom]);
      };
    }
    $doc.trigger(custom.shift(),[$that,fcustom]);
  });

  // 列表请求渲染
  $doc.on('rendering', function(event, $that) {
    var url = $that.data('url');
    var config = $that.data('config');
    var tpl = $that.data('tpl');
    var warp = $that.data('warp');
    var page = $that.data('page');
    config.page = page;
    $that.removeAttr('ht-click').text('正在加载...');
    $.ajax({
      url: url,
      type: 'get',
      dataType: 'json',
      data: config
    })
    .done(function (data) {
      var html;
        if (data.status) {
          html = template(tpl, data);
          $(warp).append(html);
            //无数据显示
          if ($.trim($(warp).html()) == "") {
              $(warp).html($("#htmlnull").html())
          }
          if(data.have) {
              $that.attr('ht-click', 'rendering').data('page', page + 1).text('点击加载更多 ');
              $that.addClass("active")
          }else{
              $that.text('没有了...');
              $that.removeClass("active")
          }
        }else{
            $that.text('没有了...');
            $that.removeClass("active")
        }
    })
    .fail(function() {
    });
  }).find('[ht-click="rendering"]').trigger(click);
  //列表请求渲染2
  var evts = 'click input change submit mouseenter mouseleave'.split(' ');
  $.each(evts, function(i, evt) {
    $doc.on(evt, '[ht-' + evt + ']', { evt : evt }, function(event) {
      var $this = $(this),
          event = event || window.event,
          custom = $this.attr('ht-' + event.data.evt ).split(',');
      for (var i = 0; i < custom.length; i++) {
        $doc.trigger(custom[i], [$this, event]);
      };
      if ($this.data('stop')) {
        event.preventDefault();
        event.stopPropagation();
      };
    });
  });

});