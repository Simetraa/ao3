﻿using AngleSharp.Dom;
using AngleSharp;
using ao3.lib;
using ao3.lib.work;

namespace ao3Tests.lib.work
{
    [TestClass()]
    public class WorkTests
    {
        private readonly string html = "<!DOCTYPE html>\r\n<html lang =\"en\">\r\n  <head>\r\n    <meta charset=\"utf-8\" />\r\n    <meta http-equiv=\"x-ua-compatible\" content=\"ie=edge\" />\r\n    <meta name=\"keywords\" content=\"fanfiction, transformative works, otw, fair use, archive\" />\r\n    <meta name=\"language\" content=\"en-US\" />\r\n    <meta name=\"subject\" content=\"fandom\" />\r\n    <meta name=\"description\" content=\"An Archive of Our Own, a project of the\r\n    Organization for Transformative Works\" />\r\n    <meta name=\"distribution\" content=\"GLOBAL\" />\r\n    <meta name=\"classification\" content=\"transformative works\" />\r\n    <meta name=\"author\" content=\"Organization for Transformative Works\" />\r\n  \t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"/>\r\n    <title>\r\n        After School - NiallWrites - Fullmetal Alchemist: Brotherhood &amp; Manga [Archive of Our Own]\r\n    </title>\r\n\r\n    <link rel=\"stylesheet\" type=\"text/css\" media=\"screen\" href=\"/stylesheets/skins/skin_1_default/1_site_screen_.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" media=\"only screen and (max-width: 62em), handheld\" href=\"/stylesheets/skins/skin_1_default/4_site_midsize.handheld_.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" media=\"only screen and (max-width: 42em), handheld\" href=\"/stylesheets/skins/skin_1_default/5_site_narrow.handheld_.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" media=\"speech\" href=\"/stylesheets/skins/skin_1_default/6_site_speech_.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" media=\"print\" href=\"/stylesheets/skins/skin_1_default/7_site_print_.css\" />\r\n<!--[if IE 8]><link rel=\"stylesheet\" type=\"text/css\" media=\"screen\" href=\"/stylesheets/skins/skin_1_default/8_site_screen_IE8_or_lower.css\" /><![endif]-->\r\n<!--[if IE 5]><link rel=\"stylesheet\" type=\"text/css\" media=\"screen\" href=\"/stylesheets/skins/skin_1_default/9_site_screen_IE5.css\" /><![endif]-->\r\n<!--[if IE 6]><link rel=\"stylesheet\" type=\"text/css\" media=\"screen\" href=\"/stylesheets/skins/skin_1_default/10_site_screen_IE6.css\" /><![endif]-->\r\n<!--[if IE 7]><link rel=\"stylesheet\" type=\"text/css\" media=\"screen\" href=\"/stylesheets/skins/skin_1_default/11_site_screen_IE7.css\" /><![endif]-->\r\n\r\n\r\n<!--sandbox for developers\t-->\r\n<link rel=\"stylesheet\" href=\"/stylesheets/sandbox.css\" />\r\n\r\n\r\n\r\n<script src=\"/javascripts/livevalidation_standalone.js\"></script>\r\n\r\n<meta name=\"csrf-param\" content=\"authenticity_token\" />\r\n<meta name=\"csrf-token\" content=\"Go2UiFCwuXJTco9Ur5U1ArzBhAU5_gxB_Hnh8mT7-_EfOv3ElXu_ks0tjXSWCSJgYOfM0Nqz9nHtBaiSZ1F8MA\" />\r\n\r\n    \r\n  </head>\r\n\r\n  <body class=\"logged-out\" >\r\n    <div id=\"outer\" class=\"wrapper\">\r\n      <ul id=\"skiplinks\"><li><a href=\"#main\">Main Content</a></li></ul>\r\n      <noscript><p id=\"javascript-warning\">While we&#39;ve done our best to make the core functionality of this site accessible without JavaScript, it will work better with it enabled. Please consider turning it on!</p></noscript>\r\n\r\n<!-- BEGIN header -->\r\n\r\n<header id=\"header\" class=\"region\">\r\n\r\n  <h1 class=\"heading\">\r\n    <a href=\"/\"><span>Archive of Our Own</span><sup> beta</sup><img alt=\"Archive of Our Own\" class=\"logo\" src=\"/images/ao3_logos/logo_42.png\" /></a>\r\n  </h1>\r\n\r\n    <div id=\"login\" class=\"dropdown\">\r\n      <p class=\"user actions\">\r\n        <a id=\"login-dropdown\" href=\"/users/login\">Log In</a>\r\n      </p>\r\n      <div id=\"small_login\" class=\"simple login\">\r\n\t<form class=\"new_user\" id=\"new_user_session_small\" action=\"/users/login\" accept-charset=\"UTF-8\" method=\"post\"><input type=\"hidden\" name=\"authenticity_token\" value=\"nBtVQ3G58vA6QRfUGYcoEhsltP1DT6JTfCmL-a_srMsNN-6a7sndFfM_27VPIS0Rx1_51uQf0FaytZLxoTtKFQ\" autocomplete=\"off\" />\r\n\t<dl>\r\n    <dt>\r\n      <label for=\"user_session_login_small\">User name or email:</label></dt>\r\n    <dd><input id=\"user_session_login_small\" type=\"text\" name=\"user[login]\" /></dd>\r\n    <dt><label for=\"user_session_password_small\">Password:</label></dt>\r\n    <dd><input id=\"user_session_password_small\" type=\"password\" name=\"user[password]\" /></dd>\r\n  </dl>\r\n  <p class=\"submit actions\">\r\n    <label for=\"user_remember_me_small\" class=\"action\"><input type=\"checkbox\" name=\"user[remember_me]\" id=\"user_remember_me_small\" value=\"1\" />Remember Me</label>\r\n    <input type=\"submit\" name=\"commit\" value=\"Log In\" />\r\n  </p>\r\n</form>\r\n<ul class=\"footnote actions\">\r\n  <li><a href=\"/users/password/new\">Forgot password?</a></li>\r\n    <li>\r\n      <a href=\"/invite_requests\">Get an Invitation</a>\r\n    </li>\r\n</ul>\r\n\r\n</div>\r\n\r\n    </div>\r\n\r\n  <nav aria-label=\"Site\">\r\n    <ul class=\"primary navigation actions\">\r\n      <li class=\"dropdown\">\r\n        <a href=\"/menu/fandoms\">Fandoms</a>\r\n        <ul class=\"menu\">\r\n  <li><a href=\"/media\">All Fandoms</a></li>\r\n        <li id=\"medium_5\"><a href=\"/media/Anime%20*a*%20Manga/fandoms\">Anime &amp; Manga</a></li>\r\n        <li id=\"medium_3\"><a href=\"/media/Books%20*a*%20Literature/fandoms\">Books &amp; Literature</a></li>\r\n        <li id=\"medium_4\"><a href=\"/media/Cartoons%20*a*%20Comics%20*a*%20Graphic%20Novels/fandoms\">Cartoons &amp; Comics &amp; Graphic Novels</a></li>\r\n        <li id=\"medium_7\"><a href=\"/media/Celebrities%20*a*%20Real%20People/fandoms\">Celebrities &amp; Real People</a></li>\r\n        <li id=\"medium_2\"><a href=\"/media/Movies/fandoms\">Movies</a></li>\r\n        <li id=\"medium_6\"><a href=\"/media/Music%20*a*%20Bands/fandoms\">Music &amp; Bands</a></li>\r\n        <li id=\"medium_8\"><a href=\"/media/Other%20Media/fandoms\">Other Media</a></li>\r\n        <li id=\"medium_30198\"><a href=\"/media/Theater/fandoms\">Theater</a></li>\r\n        <li id=\"medium_1\"><a href=\"/media/TV%20Shows/fandoms\">TV Shows</a></li>\r\n        <li id=\"medium_476\"><a href=\"/media/Video%20Games/fandoms\">Video Games</a></li>\r\n        <li id=\"medium_9971\"><a href=\"/media/Uncategorized%20Fandoms/fandoms\">Uncategorized Fandoms</a></li>\r\n</ul>\r\n\r\n      </li>\r\n      <li class=\"dropdown\">\r\n        <a href=\"/menu/browse\">Browse</a>\r\n        <ul class=\"menu\">\r\n  <li><a href=\"/works\">Works</a></li>\r\n  <li><a href=\"/bookmarks\">Bookmarks</a></li>\r\n  <li><a href=\"/tags\">Tags</a></li>\r\n  <li><a href=\"/collections\">Collections</a></li>\r\n</ul>\r\n\r\n      </li>\r\n      <li class=\"dropdown\">\r\n        <a href=\"/menu/search\">Search</a>\r\n        <ul class=\"menu\">\r\n  <li><a href=\"/works/search\">Works</a></li>\r\n  <li><a href=\"/bookmarks/search\">Bookmarks</a></li>\r\n  <li><a href=\"/tags/search\">Tags</a></li>\r\n  <li><a href=\"/people/search\">People</a></li>\r\n</ul>\r\n\r\n      </li>\r\n      <li class=\"dropdown\">\r\n        <a href=\"/menu/about\">About</a>\r\n        <ul class=\"menu\">\r\n  <li><a href=\"/about\">About Us</a></li>\r\n  <li><a href=\"/admin_posts\">News</a></li>\r\n  <li><a href=\"/faq\">FAQ</a></li>\r\n  <li><a href=\"/wrangling_guidelines\">Wrangling Guidelines</a></li>\r\n  <li><a href=\"/donate\">Donate or Volunteer</a></li>\r\n</ul>\r\n\r\n      </li>\r\n      <li class=\"search\"><form class=\"search\" id=\"search\" role=\"search\" aria-label=\"Work\" action=\"/works/search\" accept-charset=\"UTF-8\" method=\"get\">\r\n  <fieldset>\r\n    <p>\r\n      <label class=\"landmark\" for=\"site_search\">Work Search</label>\r\n      <input class=\"text\" id=\"site_search\" aria-describedby=\"site_search_tooltip\" type=\"text\" name=\"work_search[query]\" />\r\n      <span class=\"tip\" role=\"tooltip\" id=\"site_search_tooltip\">tip: &quot;sherlock (tv)&quot; m/m NOT &quot;sherlock holmes/john watson&quot;</span>\r\n      <span class=\"submit actions\"><input type=\"submit\" value=\"Search\" class=\"button\" /></span>\r\n    </p>\r\n  </fieldset>\r\n</form></li>\r\n    </ul>\r\n  </nav>\r\n\r\n\r\n\r\n  <div class=\"clear\"></div>\r\n\r\n</header>\r\n\r\n\r\n\r\n<!-- END header -->\r\n\r\n      <div id=\"inner\" class=\"wrapper\">\r\n        <!-- BEGIN sidebar -->\r\n        <!-- END sidebar -->\r\n\r\n        <!-- BEGIN main -->\r\n        <div id=\"main\" class=\"works-show region\" role=\"main\">\r\n          \r\n          <div class=\"flash\"></div>\r\n          <!--page description, messages-->\r\n<ul class=\"landmark skip\">\r\n  <li><a name=\"top\">&nbsp;</a></li>\r\n  <li><a href=\"#work\">Skip header</a></li>\r\n</ul>\r\n\r\n<!--/descriptions-->\r\n\r\n<!--subnav-->\r\n<!--/subnav-->\r\n\r\n<!-- BEGIN revealed -->\r\n<!-- BEGIN work -->\r\n<!--work description, metadata, notes and messages-->\r\n    <!-- BEGIN navigation -->\r\n<h3 class=\"landmark heading\">Actions</h3>\r\n<ul class=\"work navigation actions\" role=\"menu\">\r\n\r\n\r\n\r\n\r\n\r\n\r\n  <li class=\"comments\" id=\"show_comments_link_top\">\r\n      <a data-remote=\"true\" href=\"/comments/show_comments?view_full_work=true&amp;work_id=32265931\">Comments </a>\r\n  </li>\r\n\r\n\r\n\r\n    <li class=\"share hidden\">\r\n      <a class=\" modal\" title=\"Share Work\" href=\"/works/32265931/share\">Share</a>\r\n    </li>\r\n\r\n\r\n    <li class=\"download\" aria-haspopup=\"true\">\r\n      <a href=\"#\">Download</a>\r\n      <ul class=\"expandable secondary\">\r\n          <li><a href=\"/downloads/32265931/After_School.azw3?updated_at=1624998661\">AZW3</a></li>\r\n          <li><a href=\"/downloads/32265931/After_School.epub?updated_at=1624998661\">EPUB</a></li>\r\n          <li><a href=\"/downloads/32265931/After_School.mobi?updated_at=1624998661\">MOBI</a></li>\r\n          <li><a href=\"/downloads/32265931/After_School.pdf?updated_at=1624998661\">PDF</a></li>\r\n          <li><a href=\"/downloads/32265931/After_School.html?updated_at=1624998661\">HTML</a></li>\r\n      </ul>\r\n    </li>\r\n</ul>\r\n<!-- END navigation -->\r\n\r\n\r\n<h3 class=\"landmark heading\">Work Header</h3>\r\n\r\n<div class=\"wrapper\">\r\n\r\n  <dl class=\"work meta group\">\r\n          <dt class=\"rating tags\">\r\n\r\n              Rating:\r\n          </dt>\r\n\r\n          <dd class=\"rating tags\">\r\n            <ul class=\"commas\">\r\n               <li><a class=\"tag\" href=\"/tags/General%20Audiences/works\">General Audiences</a></li>\r\n            </ul>\r\n          </dd>\r\n          <dt class=\"warning tags\">\r\n\r\n              <a href=\"/tos_faq#tags\">Archive Warning</a>:\r\n          </dt>\r\n\r\n          <dd class=\"warning tags\">\r\n            <ul class=\"commas\">\r\n               <li><a class=\"tag\" href=\"/tags/No%20Archive%20Warnings%20Apply/works\">No Archive Warnings Apply</a></li>\r\n            </ul>\r\n          </dd>\r\n          <dt class=\"category tags\">\r\n\r\n              Category:\r\n          </dt>\r\n\r\n          <dd class=\"category tags\">\r\n            <ul class=\"commas\">\r\n               <li><a class=\"tag\" href=\"/tags/F*s*M/works\">F/M</a></li>\r\n            </ul>\r\n          </dd>\r\n          <dt class=\"fandom tags\">\r\n\r\n              Fandom:\r\n          </dt>\r\n\r\n          <dd class=\"fandom tags\">\r\n            <ul class=\"commas\">\r\n               <li><a class=\"tag\" href=\"/tags/Fullmetal%20Alchemist:%20Brotherhood%20*a*%20Manga/works\">Fullmetal Alchemist: Brotherhood &amp; Manga</a></li>\r\n            </ul>\r\n          </dd>\r\n          <dt class=\"relationship tags\">\r\n\r\n              Relationship:\r\n          </dt>\r\n\r\n          <dd class=\"relationship tags\">\r\n            <ul class=\"commas\">\r\n               <li><a class=\"tag\" href=\"/tags/Lan%20Fan*s*Ling%20Yao/works\">Lan Fan/Ling Yao</a></li>\r\n            </ul>\r\n          </dd>\r\n          <dt class=\"character tags\">\r\n\r\n              Characters:\r\n          </dt>\r\n\r\n          <dd class=\"character tags\">\r\n            <ul class=\"commas\">\r\n               <li><a class=\"tag\" href=\"/tags/Lan%20Fan%20(Fullmetal%20Alchemist)/works\">Lan Fan (Fullmetal Alchemist)</a></li><li><a class=\"tag\" href=\"/tags/Ling%20Yao/works\">Ling Yao</a></li>\r\n            </ul>\r\n          </dd>\r\n          <dt class=\"freeform tags\">\r\n\r\n              Additional Tags:\r\n          </dt>\r\n\r\n          <dd class=\"freeform tags\">\r\n            <ul class=\"commas\">\r\n               <li><a class=\"tag\" href=\"/tags/Alternate%20Universe%20-%20Modern%20Setting/works\">Alternate Universe - Modern Setting</a></li><li><a class=\"tag\" href=\"/tags/Alternate%20Universe%20-%20High%20School/works\">Alternate Universe - High School</a></li><li><a class=\"tag\" href=\"/tags/Short%20One%20Shot/works\">Short One Shot</a></li><li><a class=\"tag\" href=\"/tags/Short%20*a*%20Sweet/works\">Short &amp; Sweet</a></li><li><a class=\"tag\" href=\"/tags/Drabble/works\">Drabble</a></li><li><a class=\"tag\" href=\"/tags/Fluff/works\">Fluff</a></li>\r\n            </ul>\r\n          </dd>\r\n\r\n      <dt class=\"language\">\r\n        Language:\r\n      </dt>\r\n      <dd class=\"language\" lang=\"en\">\r\n        English\r\n      </dd>\r\n\r\n\r\n\r\n    <dt class=\"stats\">Stats:</dt>\r\n    <dd class=\"stats\">\r\n<!-- end of cache -->\r\n\r\n      <dl class=\"stats\"><dt class=\"published\">Published:</dt><dd class=\"published\">2021-06-29</dd><dt class=\"words\">Words:</dt><dd class=\"words\">466</dd><dt class=\"chapters\">Chapters:</dt><dd class=\"chapters\">1/1</dd><dt class=\"comments\">Comments:</dt><dd class=\"comments\">4</dd><dt class=\"kudos\">Kudos:</dt><dd class=\"kudos\">26</dd><dt class=\"hits\">Hits:</dt><dd class=\"hits\">297</dd></dl>\r\n      </dd>\r\n  </dl>\r\n</div>\r\n\r\n\r\n\r\n<!-- BEGIN section where work skin applies -->\r\n<div id=\"workskin\">\r\n  <div class=\"preface group\">\r\n    <h2 class=\"title heading\">\r\n      After School\r\n    </h2>\r\n    <h3 class=\"byline heading\">\r\n      <a rel=\"author\" href=\"/users/NiallWrites/pseuds/NiallWrites\">NiallWrites</a>\r\n    </h3>\r\n\r\n\r\n        <div class=\"summary module\">\r\n          <h3 class=\"heading\">Summary:</h3>\r\n            <blockquote class=\"userstuff\">\r\n              <p>A short drabble of some modern high school AU Lingfan (how original amirite)</p>\r\n            </blockquote>\r\n        </div>\r\n\r\n        <div class=\"notes module\">\r\n  <h3 class=\"heading\">Notes:</h3>\r\n\r\n\r\n\r\n\r\n\r\n    <blockquote class=\"userstuff\">\r\n      <p>Hey, sorry that it's been a little bit since I've posted but with summer rolling around I'll hopefully be able to get a fic or two posted while I'm off. Anyways, I watched FMAB recently and really enjoyed it! I definitely wanna write some more stuff with it in the future but for now here's a lil drabble with Ling and Lan Fan I whipped up:</p>\r\n    </blockquote>\r\n\r\n</div>\r\n\r\n\r\n  </div>\r\n\r\n\r\n<!--/descriptions-->\r\n\r\n<!--chapter content-->\r\n    <div id=\"chapters\" role=\"article\">\r\n        <h3 class=\"landmark heading\" id=\"work\">Work Text:</h3>\r\n          <div class=\"userstuff\"><p>\r\n  <span>“Lan Fan!”</span>\r\n</p><p>\r\n  <span>Lan Fan jumped as she heard her name get called so loud, even if she did recognise who was calling it. She looked behind her to see Ling jogging down the schoolyard towards her, his trademark beam of a smile clear on his face. Though she was blushing lightly from the loud call, she managed to give him a small smile of her own as he approached; she found it impossible not to let her mood brighten up at least a little around him.</span>\r\n</p><p>\r\n  <span>“Hey!” he greeted her cheerfully as he slowed to a stop beside her.</span>\r\n</p><p>\r\n  <span>“Hi,” she replied somewhat meekly as the two began walking together.</span>\r\n</p><p>\r\n  <span>“You free to hang out today?” Ling asked, his bright smile wavering somewhat as he added, “Winry’s busy, and Ed and Al managed to get a detention from Mustang </span>\r\n  <em>\r\n    <span>again</span>\r\n  </em>\r\n  <span>,” he explained with an exaggerated sigh.</span>\r\n</p><p>\r\n  <span>Lan Fan stifled a chuckle at the way he said so, enjoying the energy he was able to put into it. Her holding back her laughter managed to brighten Ling’s smile back up too. “W-well sorry, but I’ve got fencing today,” she answered, sheepishly scratching her cheek.</span>\r\n</p><p>\r\n  <span>Ling let out another heavy, slightly exaggerated sigh at her response, hanging his head.</span>\r\n</p><p>\r\n  <span>Thinking on her feet, Lan Fan rather hastily added, “I… wouldn’t mind doing something afterwards, though, if you’d still be up for it… We could walk home together, at least,” she suggested.</span>\r\n</p><p>\r\n  <span>As she spoke, Ling looked at her, a warmer smile now on his face. He gave a small chuckle as he took a step towards her, “Sounds good,” he replied simply, pressing a small kiss to her cheek.</span>\r\n</p><p>\r\n  <span>Lan Fan seemed to freeze on the spot, a crimson blush overtaking her face as she registered the kiss.</span>\r\n</p><p>\r\n  <span>Ling chuckled once again as he watched her react. “You’re the best, good luck with fencing!” he called, beginning to jog off once again with the same beaming smile he’d wore when approaching her back on his face.</span>\r\n</p><p>\r\n  <span>Lan Fan stayed still for almost a minute before placing a hand on the cheek he’d kissed. It still felt like it was overflowing with warmth, as did her chest. She took a deep breath, letting a smile show through on her reddened face as she exhaled. She loved a lot of things Ling did, but when they were directed at her it was enough to leave her simply overwhelmed. She knew it was something she had to work on if she wanted to return his affections but for now, she didn’t hate this feeling; this raging flusterment, this overwhelming warmth, this sensation that he left her with anytime he got even a little close to her. Ling could effortlessly leave Lan Fan positively smitten, and she felt just fine with it…</span>\r\n</p></div>\r\n        <!-- end cache -->\r\n    </div>\r\n<!--/chapter-->\r\n\r\n\r\n  </div>\r\n  <!-- END work skin -->\r\n<!-- END work -->\r\n\r\n<!-- BEGIN comment section -->\r\n<!-- Gets embedded anywhere we need to list comments on a top-level commentable. We need the local variable \"commentable\" here. -->\r\n<div id=\"feedback\" class=\"feedback\">\r\n\r\n  <h3 class=\"landmark heading\">Actions</h3>\r\n\r\n  <ul class=\"actions\" role=\"navigation\">\r\n      <li><a href=\"#main\">&#8593; Top</a></li>\r\n\r\n\r\n\r\n\r\n\r\n\r\n      <li>\r\n        <form id=\"new_kudo\" action=\"/kudos\" accept-charset=\"UTF-8\" method=\"post\"><input type=\"hidden\" name=\"authenticity_token\" value=\"gUb-acj7v3xjXzgae1gOE2vj3I69rcc5llcj5M6LZfkuodL47fVPsNvZWZ1UqVG6LodIJUn3qaL1MWNjEG3-rA\" autocomplete=\"off\" />\r\n          <input value=\"32265931\" autocomplete=\"off\" type=\"hidden\" name=\"kudo[commentable_id]\" id=\"kudo_commentable_id\" />\r\n          <input value=\"Work\" autocomplete=\"off\" type=\"hidden\" name=\"kudo[commentable_type]\" id=\"kudo_commentable_type\" />\r\n          <input type=\"submit\" name=\"commit\" value=\"Kudos ♥\" id=\"kudo_submit\" />\r\n</form>      </li>\r\n\r\n\r\n\r\n\r\n        <li id=\"show_comments_link\"><a data-remote=\"true\" href=\"/comments/show_comments?view_full_work=true&amp;work_id=32265931\">Comments (4)</a></li>\r\n  </ul>\r\n\r\n\r\n  <div id=\"kudos_message\"></div>\r\n  \r\n\r\n    <h3 class=\"landmark heading\">Kudos</h3>\r\n<div id=\"kudos\">\r\n      <p class=\"kudos\">\r\n          <a href=\"/users/Crazy_Catholic_Fangirl\">Crazy_Catholic_Fangirl</a>, <a href=\"/users/UJE\">UJE</a>, <a href=\"/users/genderselkie\">genderselkie</a>, <a href=\"/users/just_trying_my_best_everyday\">just_trying_my_best_everyday</a>, <a href=\"/users/kathkath\">kathkath</a>, <a href=\"/users/Reira21\">Reira21</a>, <a href=\"/users/echoeves\">echoeves</a>, <a href=\"/users/VelvetThunder28\">VelvetThunder28</a>, <a href=\"/users/FoggyFeather\">FoggyFeather</a>, <a href=\"/users/HalfRimzlover\">HalfRimzlover</a>, <a href=\"/users/Ship_Collector\">Ship_Collector</a>, <a href=\"/users/Animecat9000\">Animecat9000</a>, <a href=\"/users/pomelopomelo\">pomelopomelo</a>, <a href=\"/users/Chee_123\">Chee_123</a>, and <a href=\"/users/MeliDraws5\">MeliDraws5</a>\r\n             as well as \r\n          11 guests\r\n         left kudos on this work!\r\n      </p>\r\n</div>\r\n\r\n\r\n\r\n  <h3 class=\"landmark heading\"><a id=\"comments\">Comments</a></h3>\r\n  \r\n\r\n    <div id=\"add_comment_placeholder\" title=\"top level comment\">\r\n      <div id=\"add_comment\">\r\n        <!-- expects the local variables comment, commentable, and button_name -->\r\n<div class=\"post comment\" id=\"comment_form_for_32265931\">\r\n  <form class=\"new_comment\" id=\"comment_for_32265931\" action=\"/works/32265931/comments\" accept-charset=\"UTF-8\" method=\"post\"><input type=\"hidden\" name=\"authenticity_token\" value=\"YhKvcsNKAZcoKOQQ9TlB23YSuUAOJ9-Cjua4k6E8UR_9Hcb_he-R8ReWsaV9mVkppd27Bf1SrJMvF0_6l0HMIg\" autocomplete=\"off\" />\r\n    <fieldset>\r\n      <legend>Post Comment</legend>\r\n\r\n\r\n        <input type=\"hidden\" name=\"view_full_work\" id=\"view_full_work\" value=\"true\" class=\"text\" autocomplete=\"off\" />\r\n\r\n\r\n\r\n\r\n        <dl>\r\n          <dt class=\"landmark\">Note:</dt>\r\n          <dd class=\"instructions comment_form\">All fields are required. Your email address will not be published.</dd>\r\n          <dt><label for=\"comment_name_for_32265931\">Guest name</label></dt>\r\n          <dd>\r\n            <input id=\"comment_name_for_32265931\" type=\"text\" name=\"comment[name]\" />\r\n            <script>\r\n//<![CDATA[\r\nvar validation_for_comment_name_for_32265931 = new LiveValidation('comment_name_for_32265931', { wait: 500, onlyOnBlur: false });\r\nvalidation_for_comment_name_for_32265931.add(Validate.Presence, {\"failureMessage\":\"Please enter your name.\",\"validMessage\":\"\"});\r\n//]]>\r\n</script>\r\n          </dd>\r\n          <dt><label for=\"comment_email_for_32265931\">Guest email</label></dt>\r\n          <dd>\r\n            <input id=\"comment_email_for_32265931\" type=\"text\" name=\"comment[email]\" />\r\n            <script>\r\n//<![CDATA[\r\nvar validation_for_comment_email_for_32265931 = new LiveValidation('comment_email_for_32265931', { wait: 500, onlyOnBlur: false });\r\nvalidation_for_comment_email_for_32265931.add(Validate.Presence, {\"failureMessage\":\"Please enter your email address.\",\"validMessage\":\"\"});\r\n//]]>\r\n</script>\r\n          </dd>\r\n        </dl>\r\n        <p class=\"footnote\">(Plain text with limited HTML <a class=\"help symbol question modal\" title=\"Html help\" href=\"/help/html-help.html\"><span class=\"symbol question\"><span>?</span></span></a>)</p>\r\n\r\n      <p>\r\n        <label for=\"comment_content_for_32265931\" class=\"landmark\">Comment</label>\r\n        <textarea id=\"comment_content_for_32265931\" class=\"comment_form observe_textlength\" title=\"Enter Comment\" name=\"comment[comment_content]\">\r\n</textarea>\r\n        <input type=\"hidden\" id=\"controller_name_for_32265931\" name=\"controller_name\" value=\"works\" />\r\n      </p>\r\n      <p class=\"character_counter\" tabindex=\"0\"><span id=\"comment_content_for_32265931_counter\" class=\"value\" data-maxlength=\"10000\">10000</span> characters left</p>\r\n      <script>\r\n//<![CDATA[\r\nvar validation_for_comment_content_for_32265931 = new LiveValidation('comment_content_for_32265931', { wait: 500, onlyOnBlur: false });\r\nvalidation_for_comment_content_for_32265931.add(Validate.Presence, {\"failureMessage\":\"Brevity is the soul of wit, but we need your comment to have text in it.\",\"validMessage\":\"\"});\r\nvalidation_for_comment_content_for_32265931.add(Validate.Length, {\"maximum\":\"10000\",\"tooLongMessage\":\"must be less than 10000 characters long.\"});\r\n//]]>\r\n</script>\r\n      <p class=\"submit actions\">\r\n          <input type=\"submit\" name=\"commit\" value=\"Comment\" id=\"comment_submit_for_32265931\" data-disable-with=\"Please wait...\" />\r\n        </p>\r\n    </fieldset>\r\n</form></div>\r\n<div class=\"clear\"></div>\r\n\r\n      </div>\r\n    </div>\r\n\r\n  <!-- If we have javascript, here is where the comments will be spiffily inserted -->\r\n  <!-- If not, and show_comments is true, here is where the comments will be rendered -->\r\n  <div id=\"comments_placeholder\" style=\"display:none;\">\r\n  </div>\r\n\r\n\r\n</div>\r\n<!-- END comments -->\r\n\r\n<!-- END comment section -->\r\n<!-- END revealed -->\r\n\r\n\r\n\r\n          <div class=\"clear\"><!--presentational--></div>\r\n        </div>\r\n        <!-- END main -->\r\n      </div>\r\n      <!-- BEGIN footer -->\r\n<div id=\"footer\" role=\"contentinfo\" class=\"region\">\r\n  <h3 class=\"landmark heading\">Footer</h3>\r\n  <ul class=\"navigation actions\" role=\"navigation\">\r\n    <li class=\"module group\">\r\n      <h4 class=\"heading\">About the Archive</h4>\r\n      <ul class=\"menu\">\r\n        <li><a href=\"/site_map\">Site Map</a></li>\r\n        <li><a href=\"/diversity\">Diversity Statement</a></li>\r\n        <li><a href=\"/tos\">Terms of Service</a></li>\r\n        <li><a href=\"/content\">Content Policy</a></li>\r\n        <li><a href=\"/privacy\">Privacy Policy</a></li>\r\n        <li><a href=\"/dmca\">DMCA Policy</a> </li>\r\n      </ul>\r\n    </li>\r\n    <li class=\"module group\">\r\n      <h4 class=\"heading\">Contact Us</h4>\r\n      <ul class=\"menu\">\r\n        <li><a href=\"/abuse_reports/new\">Policy Questions &amp; Abuse Reports</a></li>\r\n        <li><a href=\"/support\">Technical Support &amp; Feedback</a></li>\r\n      </ul>\r\n    </li>\r\n    <li class=\"module group\">\r\n      <h4 class=\"heading\">Development</h4>\r\n      <ul class=\"menu\">\r\n          <li>\r\n            <a href=\"https://github.com/otwcode/otwarchive/commits/v0.9.387.8\">otwarchive v0.9.387.8</a>\r\n          </li>\r\n        <li><a href=\"/known_issues\">Known Issues</a></li>\r\n        <li>\r\n          <a title=\"View License\" href=\"https://www.gnu.org/licenses/old-licenses/gpl-2.0.html\">GPL-2.0-or-later</a> by the <a title=\"Organization for Transformative Works\" href=\"https://transformativeworks.org/\">OTW</a>\r\n        </li>\r\n      </ul>\r\n    </li>\r\n  </ul>\r\n</div>\r\n<!-- END footer -->\r\n\r\n    </div>\r\n    <!-- check to see if this controller/action allow tinymce before we load the gigantor js; see application_helper -->\r\n<script src=\"//ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js\" type=\"text/javascript\"></script>\r\n<script src=\"//ajax.googleapis.com/ajax/libs/jqueryui/1.10.0/jquery-ui.min.js\" type=\"text/javascript\"></script>\r\n<!-- if user has googleapis blocked for some reason we need a fallback -->\r\n<script type=\"text/javascript\">\r\n  if (typeof jQuery == 'undefined') {\r\n    document.write(unescape(\"%3Cscript src='/javascripts/jquery.min.js' type='text/javascript'%3E%3C/script%3E\"));\r\n    document.write(unescape(\"%3Cscript src='/javascripts/jquery-ui.min.js' type='text/javascript'%3E%3C/script%3E\"));\r\n  }\r\n</script>\r\n\r\n\r\n<script type=\"text/javascript\">$j = jQuery.noConflict();</script>\r\n<script src=\"/javascripts/jquery.scrollTo.min.js\"></script>\r\n<script src=\"/javascripts/jquery.livequery.min.js\"></script>\r\n<script src=\"/javascripts/rails.js\"></script>\r\n<script src=\"/javascripts/application.js\"></script>\r\n<script src=\"/javascripts/bootstrap/bootstrap-dropdown.min.js\"></script>\r\n<script src=\"/javascripts/jquery-shuffle.js\"></script>\r\n<script src=\"/javascripts/jquery.tokeninput.min.js\"></script>\r\n<script src=\"/javascripts/jquery.trap.min.js\"></script>\r\n<script src=\"/javascripts/ao3modal.min.js\"></script>\r\n<script src=\"/javascripts/js.cookie.min.js\"></script>\r\n\r\n<script src=\"/javascripts/filters.min.js\"></script>\r\n\r\n\r\n  <script>\r\n//<![CDATA[\r\n\r\n    // We can't rely on !window.localStorage to test localStorage support in\r\n    // browsers like Safari 9, which technically support it, but which have a\r\n    // storage length of 0 in private mode.\r\n    // Credit: https://github.com/getgrav/grav-plugin-admin/commit/cfe2188f10c4ca604e03c96f3e21537fda1cdf9a\r\n    function isSupported() {\r\n        var item = \"localStoragePolyfill\";\r\n        try {\r\n            localStorage.setItem(item, item);\r\n            localStorage.removeItem(item);\r\n            return true;\r\n        } catch (e) {\r\n            return false;\r\n        }\r\n    }\r\n\r\n    function acceptTOS() {\r\n      if (isSupported()) {\r\n        localStorage.setItem(\"accepted_tos\", \"20241119\");\r\n      } else {\r\n        Cookies.set(\"accepted_tos\", \"20241119\", { expires: 365 });\r\n      }\r\n    }\r\n\r\n    $j(document).ready(function() {\r\n        if (localStorage.getItem(\"accepted_tos\") !== \"20241119\" && Cookies.get(\"accepted_tos\") !== \"20241119\") {\r\n          $j(\"body\").prepend(\"<div id=\\\"tos_prompt\\\" class=\\\"hidden\\\">\\n  <h2 class=\\\"heading\\\">\\n    <span>Archive of Our Own<\\/span>\\n  <\\/h2>\\n  <div class=\\\"agreement\\\">\\n    <p>\\n      On the Archive of Our Own (AO3), users can create works, bookmarks, comments, tags, and other <a href=\\\"/tos_faq#define_content\\\">Content<\\/a>. Any information you publish on AO3 may be accessible by the public, AO3 users, and/or AO3 personnel. Be mindful when sharing personal information, including but not limited to your name, email, age, location, personal relationships, gender or sexual identity, racial or ethnic background, religious or political views, and/or account usernames for other sites.\\n    <\\/p>\\n    <p>\\n      To learn more, check out our <a href=\\\"/tos\\\">Terms of Service<\\/a>, including the <a href=\\\"/content\\\">Content Policy<\\/a> and <a href=\\\"/privacy\\\">Privacy Policy<\\/a>.\\n    <\\/p>\\n\\n    <p class=\\\"confirmation\\\">\\n      <input type=\\\"checkbox\\\" id=\\\"tos_agree\\\" />\\n      <label for=\\\"tos_agree\\\">I have read &amp; understood the 2024 Terms of Service, including the Content Policy and Privacy Policy.<\\/label>\\n    <\\/p>\\n\\n    <p class=\\\"confirmation\\\">\\n      <input type=\\\"checkbox\\\" id=\\\"data_processing_agree\\\" />\\n      <label for=\\\"data_processing_agree\\\">By checking this box, you consent to the processing of your personal data in the United States and other jurisdictions in connection with our provision of AO3 and its related services to you. You acknowledge that the data privacy laws of such jurisdictions may differ from those provided in your jurisdiction. For more information about how your personal data will be processed, please refer to our Privacy Policy.<\\/label>\\n    <\\/p>\\n\\n      <p class=\\\"submit\\\">\\n        <button name=\\\"button\\\" type=\\\"button\\\" disabled=\\\"disabled\\\" id=\\\"accept_tos\\\">I agree/consent to these Terms<\\/button>\\n      <\\/p>\\n\\n  <\\/div>\\n<\\/div>\\n\\n<script>\\n//<![CDATA[\\n\\n  \\$j(document).ready(function() {\\n    var container = \\$j(\\\"#tos_prompt\\\");\\n    var outer = \\$j(\\\"#outer\\\");\\n    var button = \\$j(\\\"#accept_tos\\\");\\n    var tosCheckbox = document.getElementById(\\\"tos_agree\\\");\\n    var dataProcessingCheckbox = document.getElementById(\\\"data_processing_agree\\\");\\n\\n    var checkboxClicked = function() {\\n      button.attr(\\\"disabled\\\", !tosCheckbox.checked || !dataProcessingCheckbox.checked);\\n      if (this.checked) {\\n        button.on(\\\"click\\\", function() {\\n          acceptTOS();\\n          outer.removeClass(\\\"hidden\\\").removeAttr(\\\"aria-hidden\\\");\\n          \\$j.when(container.fadeOut(500)).done(function() {\\n            container.remove();\\n          });\\n        });\\n      };\\n    };\\n\\n    setTimeout(showTOSPrompt, 1500);\\n\\n    function showTOSPrompt() {\\n      \\$j.when(container.fadeIn(500)).done(function() {\\n        outer.addClass(\\\"hidden\\\").attr(\\\"aria-hidden\\\", \\\"true\\\");\\n      });\\n\\n      \\$j(\\\"#tos_agree\\\").on(\\\"click\\\", checkboxClicked).change();\\n      \\$j(\\\"#data_processing_agree\\\").on(\\\"click\\\", checkboxClicked).change();\\n    };\\n  });\\n\\n//]]]]><![CDATA[>\\n<\\/script>\");\r\n        }\r\n    });\r\n\r\n//]]>\r\n</script>\r\n  <script>\r\n//<![CDATA[\r\n\r\n    $j(document).ready(function() {\r\n      var permitted_hosts = [\"104.153.64.122\",\"208.85.241.152\",\"208.85.241.157\",\"ao3.org\",\"archiveofourown.com\",\"archiveofourown.gay\",\"archiveofourown.net\",\"archiveofourown.org\",\"download.archiveofourown.org\",\"insecure.archiveofourown.org\",\"secure.archiveofourown.org\",\"www.archiveofourown.com\",\"www.archiveofourown.net\",\"www.archiveofourown.org\",\"www.ao3.org\",\"archive.transformativeworks.org\",\"xn--iao3-lw4b.ws\"];\r\n      var current_host = window.location.hostname;\r\n\r\n      if (!permitted_hosts.includes(current_host) && Cookies.get(\"proxy_notice\") !== \"0\" && window.location.protocol !== \"file:\") {\r\n        $j(\"#skiplinks\").after(\"<div id=\\\"proxy-notice\\\">\\n  <div class=\\\"userstuff\\\">\\n    <p class=\\\"important\\\">Important message:<\\/p>\\n    <ol>\\n      <li>You are using a proxy site that is not part of the Archive of Our Own.<\\/li>\\n      <li>The entity that set up the proxy site can see what you submit, including your IP address. If you log in through the proxy site, it can see your password.<\\/li>\\n    <\\/ol>\\n    <p class=\\\"important\\\">重要提示：<\\/p>\\n    <ol>\\n      <li>您使用的是第三方开发的反向代理网站，此网站并非Archive of Our Own - AO3（AO3作品库）原站。<\\/li>\\n      <li>代理网站的开发者能够获取您上传至该站点的全部内容，包括您的ip地址。如您通过代理登录AO3，对方将获得您的密码。<\\/li>\\n    <\\/ol>\\n    <p class=\\\"submit\\\"><button class=\\\"action\\\" type=\\\"button\\\" id=\\\"proxy-notice-dismiss\\\">Dismiss Notice<\\/button><\\/p>\\n  <\\/div>\\n<\\/div>\\n\\n<script>\\n//<![CDATA[\\n\\n  \\$j(document).ready(function() {\\n    \\$j(\\\"#proxy-notice-dismiss\\\").on(\\\"click\\\", function() {\\n      Cookies.set(\\\"proxy_notice\\\", \\\"0\\\");\\n      \\$j(\\\"#proxy-notice\\\").slideUp();\\n    });\\n  });\\n\\n//]]]]><![CDATA[>\\n<\\/script>\");\r\n      }\r\n    });\r\n\r\n//]]>\r\n</script>\r\n    <script>\r\n      $j(document).on(\"loadedCSRF\", function() {\r\n        function send() {\r\n          $j.post(\"/works/32265931/hit_count.json\")\r\n        }\r\n\r\n        // If a browser doesn't support prerendering, then document.prerendering\r\n        // will be undefined, and we'll just send the hit count immediately.\r\n        if (document.prerendering) {\r\n          document.addEventListener(\"prerenderingchange\", send);\r\n        } else {\r\n          send();\r\n        }\r\n      })\r\n    </script>\r\n\r\n\r\n  <script>(function(){function c(){var b=a.contentDocument||a.contentWindow.document;if(b){var d=b.createElement('script');d.innerHTML=\"window.__CF$cv$params={r:'900efc9a5ac894df',t:'MTczNjcwNDIwNC4wMDAwMDA='};var a=document.createElement('script');a.nonce='';a.src='/cdn-cgi/challenge-platform/scripts/jsd/main.js';document.getElementsByTagName('head')[0].appendChild(a);\";b.getElementsByTagName('head')[0].appendChild(d)}}if(document.body){var a=document.createElement('iframe');a.height=1;a.width=1;a.style.position='absolute';a.style.top=0;a.style.left=0;a.style.border='none';a.style.visibility='hidden';document.body.appendChild(a);if('loading'!==document.readyState)c();else if(window.addEventListener)document.addEventListener('DOMContentLoaded',c);else{var e=document.onreadystatechange||function(){};document.onreadystatechange=function(b){e(b);'loading'!==document.readyState&&(document.onreadystatechange=e,c())}}}})();</script></body>\r\n</html>\r\n";

        [TestMethod()]
        public async Task ParseFromWorkTest()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(html));


            var work = Work.ParseFromWork(document);
            Assert.AreEqual("NiallWrites", work.AuthorString);
            Assert.AreEqual(null, work.Updated);
            Assert.AreEqual(new DateOnly(2021, 6, 29), work.Published);
            CollectionAssert.AreEqual(new List<string> { "Fullmetal Alchemist: Brotherhood & Manga" }, work.Fandoms.ToList());
            CollectionAssert.AreEqual(new List<string> { "Lan Fan/Ling Yao" }, work.Relationships.ToList());
            CollectionAssert.AreEqual(new List<string> { "Lan Fan (Fullmetal Alchemist)", "Ling Yao", }, work.Characters.ToList());
            CollectionAssert.AreEqual(new List<string>{
                "Alternate Universe - Modern Setting",
                "Alternate Universe - High School",
                "Short One Shot",
                "Short & Sweet",
                "Drabble",
                "Fluff" }, work.FreeformTags.ToList());
            CollectionAssert.AreEqual(new List<Warning> { Warning.NoArchiveWarningsApply }, work.ArchiveWarnings.ToList());
            CollectionAssert.AreEqual(new List<Category> { Category.FM }, work.Categories.ToList());
            Assert.AreEqual(work.Id, 32265931);
            Assert.AreEqual("A short drabble of some modern high school AU Lingfan (how original amirite)", work.Description);
            Assert.AreEqual("After School", work.Title);
            Assert.AreEqual("English", work.Language);
            Assert.AreEqual(466, work.Words);
            Assert.AreEqual(1, work.CompletedChapters);
            Assert.AreEqual(1, work.TotalChapters);
            Assert.AreEqual(26, work.Kudos);
            Assert.AreEqual(297, work.Hits);
            Assert.AreEqual(Rating.GeneralAudiences, work.Rating);
            Assert.AreEqual(true, work.Completed);
        }
    }
}

