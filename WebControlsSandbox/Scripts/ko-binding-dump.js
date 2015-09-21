/*
 * Dump binding values onto the page using this HTML syntax:
 * <div data-bind="dump: $data"></div>
 */


/* catch and precent cyclical references in rootObject */
function toJSON(rootObject, replacer, spacer) {
  var cache = [];
  var plainJavaScriptObject = ko.toJS(rootObject);
  var replacerFunction = replacer || cycleReplacer;
  var output = ko.utils.stringifyJson(plainJavaScriptObject, replacerFunction, spacer || 2);
  cache = null;
  return output;
  
  function cycleReplacer(key, value) {
    if (typeof value === 'object' && value !== null) {
      if (cache.indexOf(value) !== -1) {
        return; // cycle is found, skip it
      }
      cache.push(value);
    }
    return value;
  }
}

ko.bindingHandlers.dump = {
  init: function(element, valueAccessor, allBindingsAccessor, viewmodel, bindingContext) {
    var context = valueAccessor();
    var allBindings = allBindingsAccessor();
    var pre = document.createElement('pre');
    
    element.appendChild(pre);
    
    var dumpJSON = ko.computed({
      read: function() {
        var enable = allBindings.enable === undefined || allBindings.enable;
        return enable ? toJSON(context, null, 2): '';
      },
      disposeWhenNodeisRemoved: element
    });
    
    ko.applyBindingsToNode(pre, { 
      text: dumpJSON, 
      visible: dumpJSON 
    });
    
    return { controlsDescendentBindings: true };
  }
};