// -------------- Many tweets + reply box ------------------------------
function checkVisibilityReplyControls(id) {
    var repliesControls = $('#tweet-replies-controls-' + id);
    var replyBox = $('#tweet-reply-box-only-' + id);

    if (replyBox.is(":visible")) {
        $(replyBox).hide("slow");
    }

    if ($(repliesControls).is(":visible")) {
        $(repliesControls).hide("slow");
    } else {
        $(repliesControls).show("slow");
        loadReplyBoxForCommentsTree(id);

        createTooltips();
    }
}

function loadReplyBoxForCommentsTree(id) {
    $.ajax({
        url: '/tweets/postreply',
        data: { id: id }
    }).done(function (result) {
        $('#add-reply-' + id).replaceWith(result);
    })
}

// -------------- Single reply box --------------------------------
function checkVisibilityReplyBox(id) {
    var repliesControls = $('#tweet-replies-controls-' + id);
    var replyBox = $('#tweet-reply-box-only-' + id);

    if (repliesControls.is(":visible")) {
        $(repliesControls).hide("slow");
    }

    if ($(replyBox).is(":visible")) {
        $(replyBox).hide("slow");
    } else {
        $(replyBox).show("slow");
        loadReplyBoxOnly(id);
    }
}

function loadReplyBoxOnly(id) {
    $.ajax({
        url: '/tweets/postreply',
        data: { id: id }
    }).done(function (result) {
        $('#add-reply-only-' + id).replaceWith(result);
    })
}


// -------------------------------------------
function postTweet(data) {
    $('.tweets-container').prepend(data.responseText);
}

// ------------------------- Track char count in tweets
function trackTweetCharCountMain(form) {
    var charCount = form.value.length;
    var tracker = $('#char-tracker-main');

    $(tracker).text(140 - charCount);

    if (parseInt($(tracker).text()) >= 0) {
        $(tracker).css("color", "green");
    } else {
        $(tracker).css("color", "red");
    }
}

function trackTweetCharCountReply(form, id) {
    var charCount = form.value.length;
    var tracker = $('#char-tracker-' + id);

    $(tracker).text(140 - charCount);

    if (parseInt($(tracker).text()) >= 0) {
        $(tracker).css("color", "green");
    } else {
        $(tracker).css("color", "red");
    }
}

// -------------------------------------
function reloadPage() {
    return location.reload(true);
}

// -------Tweet tooltips---------------------------
$(document).ready(function () {
    createTooltips();
})

function createTooltips() {
    $(document).ready(function () {
        $('.tweet-avatar').each(function () {
            $(this).qtip({
                content: {
                    text: $(this).next('div')
                },
                style: {
                    classes: 'qtip-light'
                },
                position: {
                    my: 'right center',
                    at: 'left center'
                },
                show: {
                    delay: 250
                },
                hide: {
                    fixed: true,
                    delay: 500
                }
            })
        });
    })
}
