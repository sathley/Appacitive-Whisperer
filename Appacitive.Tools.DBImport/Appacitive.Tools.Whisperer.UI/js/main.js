
$(document).ready(function() {
    $('#btnVisualize').click(visualize);
});

function visualize() {
    
    // make a new graph
    var graph = new Springy.Graph();

    // make some nodes
    var spruce = graph.newNode({ label: 'Norway Spruce' });
    var fir = graph.newNode({ label: 'Sicilian Fir' });

    // connect them with an edge
    graph.newEdge(spruce, fir);
    $('#divDatabase').springy({ graph: graph });
}