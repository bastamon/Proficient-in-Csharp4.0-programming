using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace HBMISR.server
{
    /// <summary>
    /// ºº×Ö×ª»»³ÉÆ´Òô
    /// </summary>
    public class ConvertPinYin
    {
        public ConvertPinYin()
        {

        }

        #region //gb2312ÖÐµÄºº×Ö±àÂë
        //01-09ÇøÎªÌØÊâ·ûºÅ¡£ 
        //16-55ÇøÎªÒ»¼¶ºº×Ö£¬°´Æ´ÒôÅÅÐò¡£ 
        //56-87ÇøÎª¶þ¼¶ºº×Ö£¬°´²¿Ê×/±Ê»­ÅÅÐò¡£
        // Ã¿¸öºº×Ö¼°·ûºÅÒÔÁ½¸ö×Ö½ÚÀ´±íÊ¾¡£µÚÒ»¸ö×Ö½Ú³ÆÎª¡°¸ßÎ»×Ö½Ú¡±£¬µÚ¶þ¸ö×Ö½Ú³ÆÎª¡°µÍÎ»×Ö½Ú¡±¡£
        //¡°¸ßÎ»×Ö½Ú¡±Ê¹ÓÃÁË0xA1-0xF7(°Ñ01-87ÇøµÄÇøºÅ¼ÓÉÏ0xA0)£¬¡°µÍÎ»×Ö½Ú¡±Ê¹ÓÃÁË0xA1-0xFE(°Ñ01-94¼ÓÉÏ0xA0)¡£
        //ÀýÈç¡°°¡¡±×ÖÔÚ´ó¶àÊý³ÌÐòÖÐ£¬»áÒÔ0xB0A1´¢´æ¡££¨ÓëÇøÎ»Âë¶Ô±È£º0xB0=0xA0+16,0xA1=0xA0+1£©¡£

        /// <summary>
        /// Æ´Òô¶ÔÓ¦µÄËùÓÐºº×Ö
        /// </summary>
        private readonly string[][] Allhz =
        new string[][]
        {
            new string[]{"a","ß¹…°¢ºÇ°¡ëçï¹àÄåH‹ï"},
            new string[]{"ai","°¬êi´Ìš±°¥°§ÚÀ•l°®íÁ°¦ŠÖ°¤°£ÞßŠâ†³v°¨šGé^°¯œâƒù‰aœÜæÈàÈàÉ°«Û°­ôƒè¨êÓïÍ‡B°ª…¥”±”²ƒvÜt°}øËBàæ‡†‰¹‘°‹ÜžG‘¹òI°©ðgæX•á­a°Š²}ñLö°×cµKÌ@þH×rèP÷_÷oì\ìa"},
            new string[]{"an","³§¹ãŒßáí°²ó°¶^°´›¡ˆ«qÇI°³°·èñ°¸°±Øtï§ÚÏÛûˆÝ‚¹ÈC‹Fë@âÖ†††±‹jÈsÈ€Þî••°µÄWëˆ¯uðÆÁOÕYØä@ÉŽ°°ì”±QåBÖOƒ‡éœñKõc±Vùgù“í÷ö"},
            new string[]{"ang","…nÑöŒì–‹°º°¹•n°»óaál"},
            new string[]{"ao","Ø²°¼Æbá®’jÞÖˆÛêC–À°À°½—`éOÝEæÁ°Á…ëJŠS°ÂŠWâÚà»†õÊTåÛ‹‹‹®ª‡å‘REæñ°¾éá“³­HÁ°Ä°Ã‰¥ŽS´xòüñú°¿“ýÂKÖ’Ö“Ò\÷¡öËÂOÏù÷é nòˆúqö—÷`ü"},
            new string[]{"ba","°Ë”°Í…©°È°Ç–[«X°Óá±°Ñ’U’i°ÉŠBy°Å²®ÜØ°ÎŠ‚°ÖèË–ÂÃ_°jîÙ°Ìžßˆ¢°Æ†^ÍM°ÒôÎ°Õ°ÊášØ^Ú•ÝÃ°ÏÔyâZâƒÝR÷„öÑ°Ð÷ÉïTÁTõEÁjæŽõ•ü–Ò†³F°Ôþx‰Îå±™ñþw"},
            new string[]{"bai","°×°Ù²®ŽßßÂ°Û’…°ÜÄ°°Ý°Ø’“–à®B¸q”¡ÞãªWêþ»“½]°ÞƒÄ°Ú»ŸËb”[Ù”ív ÛÒo"},
            new string[]{"ban","°ì°ëÚæ°éŒê°â°çÛàˆmE°å°æ”‘•L­š°èŠ”ÃR°íîÓ–®°à¶t»{°ãô²½Oœ°°ßâkã[îC”Ê°áÎZì‡øXô‘Î†ñ£ÎŒÑ—Þk°äé›ñ­Þn°ê"},
            new string[]{"bang","°î«g°ï’²°óˆ ’ÊÅÔ°ð°öäºß™½‰°ô—”Ž°°ø°ùÝò“s‰Y¶œ°òÎM°ñ ¥ŽÀ°÷°õ¿RÅÖrŽÍæ^íD"},
            new string[]{"bao","Ùè„ô°üÅÙÜ±¨±§Aæß±¥±¦°ú±£ÅÚ–¢°ûð±·‘è˜Œp°þ„ƒ«’±ªõÀÍd¸Ç˜Ýá‹~±¤ˆóˆçìÒŒ‡öµãEï–ï’±«±¢ÑfñhøRóbñÙ±©Ê}óŽød°ý¾‹Ì™ÒJÙ…õUé–±¡ƒ˜•ÞÐˆÆÙËŒ—±¬ÆØ ÝŒšÞý_ètìd"},
            new string[]{"bei","±´±±ÚéÚýßÂ±·ØÃØ•K±¸±­ ´±°ÆpàT–È±³±µ° ±»ã£±¶‚pªN†h‚³‚Ëàf—G—f¬D¬i±¹±²±º—”—À±¯‚äƒF“dÝKÍ“ðÇ±®íÕ¶FãmÝíÊ Íì‹ócñØÕRÝ…ä^¼L‘v±ÛË÷¹ùlöÍ"},
            new string[]{"ben","º»±¾ÛÐ±¼±½›yêÚÛÎŠM‚–’Ùœ`ßGÁ±¿—L Äï¼—ñ“àåQ"},
            new string[]{"beng","Èµp±Ã±Å±Â®g°öˆ©ˆÈ±ÁßJŠRÈE±À“g¬e¬a½léa¯nÐàÔ‰l¾XéGê´¿‡ìž±Äça"},
            new string[]{"bi","Ø°±Ò±È±Ø±Ï–a±Õ‰ýÞ±ÓßÁß›ÓØ×ÜÅåþ›aˆf°n¯Hî¯®nš·ÃÚPWŠŒ±ËŒÂÆƒ…ñßÙÜê±Ý–Ä–©êÚ±Ñ«¯RïõÃØîé®…Ð‹ô°¸“»z»±Ð±ÊáùÙÂÝ©ÝÉÈ]ÈZ‚¿†žâØæ¾±Ö—aé]é[ÙSÙCÔv¹PóÙÍŒ°zµ–šÈÚPˆãÏã¹œ ±ÆåöŒ’†ô±É±ÍÉœääœüŸ•—é˜[Äbî¢²D±Ô¯wÍšñÔ±ÙõÏésïãGñEÂ¾a¹uóë¹v±ÌŸÎªŒ±ÎàŠŽÅ±×±ÇØ„ŽÆ§ÁXª‹ØPóñƒÓv¿oó÷º`Î“æÔ±ÚY±ÜÞµùSõI”Àå¨±ÛÛ‹÷”÷Âæqç@ð{àˆèµŠ`ÒgôÅÀVôxí{íSòÜLÜKösúzÚFèE–Cú‡ü„"},
            new string[]{"bian","±å±ßÛÍÜÐ’\›MãêâíáŠ±áž×«fO±ä±ãí¾•c±âñ¹‰äÒŒÙHóÖØÒ±éÌ“OÈqªpÆ±àçÂ®KìÔ ¤érñÛÞgíÜ·Hòù¼D¾œ¾ŽÅŒ¹Õ—ì™øu±æ±çÞl±èöýæQª Ëxß„ß…±ÞöcöbÞpÞq×ƒ»e"},
            new string[]{"biao","è¼±êóTì©±í÷Ô‚læ»±ëœWªYÃ ñÑ‹›æôÊEœý“¿Ž¼‰wïRä‰Õ•ûŸÏ±ì˜ËïÚÒFñ¦´‚ì­ì®ålï[ƒš™~Ë‘ždÖ€÷§çSÅAÙ™ gïð·…ïjïkïlïnèsóQ"},
            new string[]{"bie","±ð„eÇaÍrÖ•±ï±ñÒXÏhõ¿±î°T÷Mü‚Ì‹ý–"},
            new string[]{"bin","ß“çã±ö±ò—€—ÃÙÏ±óçÍ±÷±õéëéÄë÷¬žÙeÙfØhó‰ïÙîlƒ†±ôžIžM”PáÙÌžó­pÄœš›™‰žlìE÷ÆÀ_Ï™÷Þè\óxî ôW"},
            new string[]{"bing","•±ûšê²¢±ùÚûêv±øÆuKŽÕã’mT±ü±þ±úÆÁ±ýˆ—•\•mŽð‚vç®¸p±}²¡–Þ–â·’Ís’ò‚§‚ìŒ}Þð—ŠÙ÷Õ@â·AìãuïžéÄðVí@õmìh"},
            new string[]{"bo","²·°hØÃÃJ²®²µ þ²¯²¦²¨²´­“®zÃ`°Ø‚NàR²ª²£õÀ±C°ã°þÙñ›Â’©†\âÄÑB³j­”¶z²§îà²¬ÑJ²±à£œ_²¤È`²°À¼žõËð¾È•²³œÀ²©ñAö´B²«ªtã\â“ãKöÑ÷ˆÆÇñCñgÅ‡²­ÊNƒkƒ`Ÿ¹²² ¦éDÞ¬ó²¥“ÜõÛðGäcñFñ•õN±¡ŒXë¢éÞÒUžQÖcº~ðoænùP‡¥µR‘Åómópô¤¼\×LÜ@™ØÒqÌYíçò’÷Qè}"},
            new string[]{"bu","²·²»ß²²¼…ÄÑ²½²¹šhši²À…ùŽ~îÐ–¿îßŽï‹²¸m²¶’ÃÆÒˆ¶Ç[åÍ²¿„Ï²ºêÎê³Ña±¤âbâ˜øGÉžEÕcÛYðJà^º^ðXåqùL²¾ÞKõ³ûQ"},
            new string[]{"ca","k²ðßnàê²Á”cíåµg"},
            new string[]{"cai","²Å²Æ²Ä²ÉØ”‚šŒqŒuŠé²Â²Ê‚Æ²Ë†’’ñˆÆ—²ÇÛP¾Z²Ì²È¿n²ÃÀu"},
            new string[]{"can","²Ó²Î²Ð²Ïï{…¢…£æî²Ñ²Ò²ôœ’†Ðåî…¤•üšˆôÓ‚ð‘M‘KÎ]‘”‘Lƒ…‹ÛËLÓ·_²Í Nºdè²Öþp÷õ |ò‰üoÐTÐQ"},
            new string[]{"cang","²Ö¨Ø÷²Ô²×û]²Õ‚}‚áƒûÉnªÈœæê°Ï@žPÅ“Ù‰²ØúIè†"},
            new string[]{"cao","ÆHÃH²Ý•ù²Üó‘FäîÉ˜àÐæ“Ùà“²Û²ÚÒG‘¨²Ùó©ô½Ò_ç[òxü"},
            new string[]{"ce","‰÷²áƒÔ²à²ÞÇR’‘²ââü”˜³€»¸žÈY…‹‚ÈªeÅœyŽúÈm²ß¹k¹ZÉƒ¹‹‘Šºu"},
            new string[]{"cen","á¯²Î¸’ä¹ˆ¨—qß"},
            new string[]{"ceng","²ãÔøŒÓàáò¸}²äòš"},
            new string[]{"cha","²æŠg’Q’Kãâ²íè¾²ïÉ²÷ñÃÅa¼p„xæ±²ç²è²îßå²é–Ë¶gˆ“’·‚²âª†âÔû²å²ë“c¿âÇìxÔŒÔˆé«é¶ïÊ²ê²ìã˜Å‘Û‚®›àêåšðléßïïèd"},
            new string[]{"chai","ÐƒÆO åÃPîÎ²ðÙ­²îò²Ó²ò²ñÆâOµ}†¶ðûƒŠÏŠ‡Ð"},
            new string[]{"chan","²úÞ{âã•C„iÆgµ¥P”âêè›º„}ÚÆ—{²ô²ù®a®b³ƒ²÷²ûæ¿ÝÛœµìø²ó—åî²öÒ—ÑgŸž²ø„•ƒ]Iª†“½“·“˜²õÕSäaäiéˆÕ~¿C‹ÈÊræöäý¨ƒ{âÜŽÂŽÊå¤´vŸíš´ÏMàšºoÒbápÙæžeÏsÀA‘Ïó¸àž„­ƒ§çPÖêU‡ÁžŽf‘Ô‰Ê”v™ÙÀpÀséK‡Ï"},
            new string[]{"chang","³§³¤Ÿ Øö³¡ƒ¸âêÜÉ«`³¦éM³©éL²ýêÆ³¢ÛË³«ÌÈ‚t³¥ÝÅÈOáä³£³ªã®ãÑæ½²þœC®D¬dˆöŸ…—Ç³¨Äc®^è Ñm®˜ÉÑ•³‰jæÏ…”‡LƒYSÕkÄq¬ äå_é‹öðë©‡Ÿƒ”÷•íoÏ^öKü÷l"},
            new string[]{"chao","ž£³³³­€³´â÷³® Ÿ™ùêË±|ñéÓeÔN´Â³²ŽzìÌ³¬ânŸq³¯½Ëà}ü…Ÿ·R³±˜È³°ûž¸J¸SÁVü{ÞCÖšŽl"},
            new string[]{"che","³ß³µ¼³¹³¶Ü‡ÛåÞŠ‚eíº‚®Çp†qŸEÂs³Œ³³¸Í’îJ“Ý³·³ºØ„ï²u …"},
            new string[]{"chen","Ø÷³¾³¼³½³À³Â³ÁÞÞÓ³Ä¯MÆÇ_Çk³»ê–×ŸGå·³Æö³Ú’Ü•ÔH³¿”—F’×ÚÈ×—²è¡³•³ÃÚfâ\í×Ÿ‹é´àÁÊc‰}‰m˜¹”´¯„¾DÙoÕ€²_ë‰öÛ{ýY´~ÖRÏIÖnýZåŒËlƒ¡û‰Ù•‡¸Úß•æ™ÂÒrúm×"},
            new string[]{"cheng","Ø©³É–b³ÊêpàJ³Ï›„èÇ³Ð ³Çèß›“\Ç^‚DŒkŠ¿w’¬»³Ñ³Ë›Õ—¢Ûô‘³Ò³Æ³Ó¬b¬A«žîõîñÊ¢Ã”³·œòÉŽñ‚ ’Þ“Z—¼—–ñÎ½† ª±³Í³Ì•˜ˆá¹fÚWÕ\ëó‰SœË“£‘r·Q®—õ¨ä…ìl¿B‘~³Å“Î³Î¯àáéÌ³È˜û™rîªÚXîdòGõ“¸V™fžjÀ˜"},
            new string[]{"chi","³ßß³…µ³â³ÔÀ³Ø³Ú³ÛŠw³ÙâÁ…Õ–oÃLžÃ³à³ÝÛæ³Þø…qÜÝ’xÞ‹³ÖÇK„Èˆ‰p³ãÃn¸‡³Ü³áÁ‹Ãquð·ò¿ÍN„Ðßê«³×ÍhÑE®Eë·ŸU»Œí÷ó×ôùÚdÙPï†ÔW¹MšIšnßWœ‰à´†ËàÍÙÑ“¤ßgæÊ³Õ¯vÑlÄS½‚ë†ñYãMãrã‰Â@¹}‘y¯€ßoßt‡iñ¡‘JñÝš“Ü¯kÕvÂB²lõØýXø|óøŸëó¤ÖsùAå~Ú†ÑD‘´°V÷ÎùýcüJ”~¯úuü["},
            new string[]{"chong","³å³ä³æ›_âç³èÜûÖÖÖØÁˆ«–Ó¿›Òê™³çƒï¥ˆÃô©“_“›†ürã|Ñ~Îu‘o¾…ã¿ÁZÏxô¾ÛŒŒ™ ‚Ðn"},
            new string[]{"chou","³óE³ð…Á³é–ƒ–„Ù±àü–ä³ôþr¼—Ç“‚¸³ñã°½[ßc³ë—¹ÅWÔ—³êáOÑn³î°{³í³ï“ošŽ¾I³ò³ìëlõ\áhºNñ¬ƒ‰ËgŽÎ‘À‹á b²ƒ™„öÅ® »I ßÜPá~×‡×‰ô{ë| â"},
            new string[]{"chu","Û»³ö„I´¦ŒçÖú³õ¸aâðØXç©›{èÆ³ý™ú¸e´¡”™ÐóÆc‚m—ÆˆÇÌŽ½I¬Gµ—Øa³üèú´¢‚âàs³øÉZ³ú³û´¥ãIÉe³þ˜Zœä´¤ÚnÂaòÜñÒ‹ƒ Ë“¹ézäzéËN‡bƒºXÕ‘ãÀ³÷é™sƒ¦”ßšb­lÄ•÷íërµAØŒ³ùÏ{ŽÐ™Ÿ™»õéÓ|ýiúRÜXýs´£ýƒ"},
            new string[]{"chua","šH"},
            new string[]{"chuai","à¨´§ÞõþŽàÜÄuëúõßþŒ"},
            new string[]{"chuan","çÝ´¨´«šöâ¶ë°´®«[îË´©ô­ÇFÅx´¬„”âAå×´­ªk‚÷šN´ª¬Ùiƒb•Äº@ÝŽÛwúE"},
            new string[]{"chuang","„V´´´³âë´²„k —´¯„y„€·™´°„“‚ü ¡í“œ §¯´±‡l´}¸RêJ"},
            new string[]{"chui","´µ´¶´¹ˆ§–ûÚï‚…Ç”´·“€é¢×µ‡ùÄD´¸é³¹ŠåNæmîqý—"},
            new string[]{"chun","´¿•I–~ê´º¼ƒ´½Ý»›Ì´¾ÈN‚¤Ã‹‰@‹aÈoœ· ÆðÈœ÷´»˜‡ÉO•«¬tÃ²QÉ”_¹—´¼òíácåTÙƒÝ˜ê™šù‡õžöjùœ´Àþ”"},
            new string[]{"chuo","Þu†dŠÆŠÅ‹S‹Cà¨ßO›í´Â·ê¡áQÛU¾bÝzõÖöºÚ}ðU´‡“ó¿ž´Ášf‡Çýpèq"},
            new string[]{"ci","–c´Î´ËËÅ´ÊÕè´Ì„p«yìô–²ˆˆ×È›²îŽãßÚÜë´ÄÆ˜–æ´É«u°r´ÃÇ„Úeé^´Í½aÞeÔ~‚½Íy´È®N´ÇÚ´ÆðË´ÅÎˆódð@ï“Þi¿WÙnôÙËF‹ãžBøyõJµQóqÏ…Þoú]ú\ý€"},
            new string[]{"cong","´Ó´Ô´ÒÜÊ´Ñ‡èèÈòÆ‰¿S¾ÄÀŠæäÈ•ŒQ›ß—Œçý´Ð˜B‘mÊ[É|Y^æõÂ‡´ÏÂŒÙzÙ{Õp•¾œ˜º˜Ú­Bè®ŸÐ²j˜âåSæCºbÂ”ÏZËq…²ÀSÖçWçEò^ò‹žš ™ß"},
            new string[]{"cou","´Õœê£é¨ëíÝ"},
            new string[]{"cu","×äáÞI´ÙéãÃ´ÖÓcâ§õ¡‹{¯|û€ÚuÕKÝý‡m‘–È¤Ûqû‚´×Û€¯•™[¿q´Øüyõ¾õíÜAî•û›"},
            new string[]{"cuan","ÙàÇˆ´Üß¥š–Ÿä´Ûºxïé¸Z´Ú™«ÔÜ”xŽm™ç·‰Üfè‰ìà"},
            new string[]{"cui","ºõ¯Q»‚´àÃy‚yÁŒ¼´ÞÝÍßý†Ÿ´ãã²¬XÃœŸnë¥ÀŠÅ´ßƒþ´á´â‘N´Ýª‰‰…yéÁ¾\´äÚ~˜§è­Ä‹Äƒ°„´…¿\îx¸WÒPûÄ›çJ"},
            new string[]{"cun","´ç„Yß—ââ´æ´åü»v’ŽñåÛZ´‰–¸€Ä~¶×"},
            new string[]{"cuo","„v„zÇsÇu´ì‰èØÈëâÌ‘´ëßHÉc´êáÏÕï±ïó±‘ðî´í¬›ÉxßuÊPðû´èäS´éåeõºáiõãæ\àŸûz"},
            new string[]{"da","´ó‡´ò´ïšùÞ…Þ‡Þ‰…ö…ìâòæ§í³®zžØˆ™ßÕÇQÇEÞÇ®}±oðã®†µ¬ßQ…A„‘óÎ¸—´ðÔz´îàªß_ËþÛQ÷°ñ×´ñ‰¡ËR‡}“Ò÷²Áeæpæ]ÀJÏƒÜJùŒöWèNí^ý‘ý“"},
            new string[]{"dai","´ó´õšx´ú´ôß¾Þaß°çªˆ‚ææá·Ž‘þåÊ´ø´ù´ý´ûµ¡–±çéŽ¡Ø–ÜÜ–Íf½HŽ§´þÜ¤´öÙJÝDÛF¬xªyK…¦•ÎÎ}¿Døl÷ì´üÅ•‘·´÷º‰ìOžŽÒyünì^"},
            new string[]{"dan","µ¤µ©Ê¯„[·µ«ŽŠl’bµ£›µ¥µ® ý›X­«mµ¨íñ–½³NÐy…g†mµ¦¯Dµ¢Ân¼ðãñõµ°à¢†›ÝÌµ¯µ­µ¬µ§ÜláGÓgŽ‹[†Î†²ÍžÄEµªêæééð÷·žÉ…ÕQîFó‡ñd‡nà…SƒdÑóìÙÙ„é‡d—“Û“Ú‘„ÙyðZñšø}ü^‘žå£“úš—¶V®X°DÄ‘ÒRº„°QÀWÂ›‡·Ùœð…ìKülür"},
            new string[]{"dang","šëˆWÛÊµ±å´í¸ˆ›µ²µ´µ³µµ«šîõÝÐñÉ¹YÚÔ‹P®GßTÕ®”Éë‹‹´X²^ƒ}ÊŽÚÇ“õ‡Ž‰³™n±U­c­TÒdº‚µDºšÏ}Å™×[ühÌoèKêW”†žª™é×•"},
            new string[]{"dao","µ¶Þxâáß¶–]ë®µ¼hµºµ½Åsê‰µ¹uàüµ·á’÷µÁ’Òµ¿ìâµ»ÈKµÀµ”—Í±IþOô€“vëIëì˜˜ÁŸ·Rµ¾‡‹ÅŒ§ëZ™|ÏCµ¸”F‰»ŽW c¶\ÂRÜ„­ôî"},
            new string[]{"de","µØµ×µÄzµÃ›ú¿œ—‘ï½Ô‡NµÂåu"},
            new string[]{"dei","µÃ‡N"},
            new string[]{"den","’O’Y"},
            new string[]{"deng","µËµÆµÇµÈØOê­à‡ëQµÊƒ\àâ‹¿áØ³Î‰œ­OŸôïë¸~µÉíãÅ˜™žô£ÓRµÅç‹"},
            new string[]{"di","ØµµØªµÍ…}Û¡êsfµÜÚ®ˆkˆhµÒ–m«Z•AµÄÙá–šŠDKIµÖ’†Ûæ…à~µ×µµÏÆlÆm‚dµÛˆªèÜ ¹µÐ¶~íÆÐ”ˆ¯µÓµÝÞž†væ·Ý¶Ç…Çœ‚±êÚÐ††¬’ãˆ¹—\—bŸbµ±ƒ«ŸµÑµÚâKÚhÓhÔgÝBíûì{êëµÞé¦µÌÌá“W‹XµÙƒCßfœì˜N¶EóƒñVÄVãdÂ‚íÚÎ[µÔ÷¾îEµÎ“Ÿ‰—‰„ßrµÕàÖÊOÊHÊL®S˜µÎy‘d”³¾†ÛyÖBïáŽRË‹‡”ÏE´”ôÆíLØpÛ—çC‡Ã¼eÓ]ûM"},
            new string[]{"dia","àÇ"},
            new string[]{"dian","µçµéÚçµèµäŠHµêÛãµæµã‘ú”“çèîäŠû‹Lµàµíµëµì…Ž‚Ù¬U”¥—ÏµîÉ_µáÑÍŸµâëŠâš˜ˆ˜•‰|¯’ÊsõÚµåÕ‰«˜ëücÛ†´ô¡ñ°áÛîîŒñ²Žo”„ò›µßŽp°dý‚"},
            new string[]{"diao","µótµðšôÄñ’Fµõ®„aµöÍ@ŒÅ±@ÓŽ·–µòµ÷ŠP¬‡µôÍqîöï¢ážâyõõ¬hÉ‰¸uµïøJëäHã“ô†Õ{¸L²f¯šõMöôä”åcµñËyü—õ ùmèS"},
            new string[]{"die","Æ|µü†A†OU’¡À„ÛìgÃ]•ið¬±yÂWµù‘äµý±‚ñó®’Ž²à©ÂÜ¦Þé½xÅ\éPÔeÚgµøÛ@ÛLšŠµþ˜G šëºµúÑÎHÞµûÅŽÕ™õÞø‡®ÏHöøÒBÛ•èöl¯A¯BšÛ"},
            new string[]{"ding","¶¡Øê¶©ÆJŽŠ¶£Šcð—âçà¶¤î®¶¢ðÛ³GÍBñô¶¨Ó†ôúá”ï}ìwí”îúà¤Èbëë—Å¶¦¶§íÖ´Oàü‡¶¥ËYìåV´îrç–"},
            new string[]{"diu","¶ªGîûïMäA"},
            new string[]{"dong","¶«¶¬¶¯¶³Æ{ßËá´–|¶±Ûí’œ¶´¶°ë±¶²á¼ŠŸ•këËÞ“ð´ëØƒö‘ã‚”ˆÄ›ò„ÓÇ‡Šàž–¸•íÏÄL¶­œ§šæ—ƒPÎX¹šÕ‰¶®Ê‹ÙñŽõ[ëšüŠöCù…úH"},
            new string[]{"dou","¶·cêh…¼…Ê¶¶¶¹–’î×¶¸àK„r¶¼¶ÁÇW›Ã†t†zð¶ºò½ôYÃ–áHšÃ—uäÂ¶µƒÃ¶»â^ékñ¼ÝúôZðL™Xóû”ÔêL¸]ô^ô`ôa"},
            new string[]{"du","…¶… Ü¶¶Ê¶Å¶ÇŠ¶À¶È¶¾óÆÇT¶Á„†¶¼ê^¶ŠäÂ¶Â¶Ä¶Éèü¬o¶¿ë¹ì|¶½¶Ãà½¶ÆÑtÕiÓGÙ€á`ª–ªšåLºVÎ–êA”¾åƒ„E…Xš˜ž^‹ó™³šœ­{ © Ù°òy÷ò×x÷Çí~ÐCÚGØKèoó¼ííbüt×˜"},
            new string[]{"duan","¶ÎÂZ¶Ï‚ÇÈ˜‹e¶Ð‰F¶Ìé²ìÑ¬‡Äa¶Í¶Ë´VÑƒ¾„š¬óýå‘æH”àÜY»f"},
            new string[]{"dui","¶Ó¶Ôƒµƒ¶Œ¶Òµqí¡ˆŒêŒê ¶Ñ‰[¶Ø¯yíÔ½˜´qŒ¦‘‡îXåTí­žAø‹ïæËc‘»ž}×Bç…çŽ×mÞV"},
            new string[]{"dun","¯ŽÝ¶Öãç¶ÚìÀÞšíï¶Üí»¶Ûõ»ª¶Ýâg¶Ø˜JîD´]ßqÎP‰Ý“æ—¶Õ‰•Ÿõ‡ Ô“ÇÛv¶Ùïæíâ¶×ÜHÜOò—"},
            new string[]{"duo","¶áÍÔ¶à‰ï¶ä–\–mð™ãõßÍêwêy¶ç„m¶ßßá¶È’–’—¶âˆ‘–Ã–ªŒ¹–úîìþK„„„‹ˆÊ¶é¶Þ“ç¶¶æ”£”Ÿ”­šÇ¯k”¦â‡¶è‹s†Æï˜Úr¶åÛGñÖ¶ãÜo—Ù¾EŠZ‘†‰™‰š„A¿JõâÉ‡šùCùzöVôDÜ€‡¾èIŠb"},

            new string[]{"e","¶ò˜‘öŒï¶ï…ÅßÀ…°¢êqÜÃÞˆ¶ó‡êéî–•…ÙŠŠŠŽ³X³SŠ´†H†@¶íSÛÑåí¶ö›á¶ëk¶ñ°·Ý­Å¶Šãæ¹¶ð„þ³bÍLÓž³r«Ü—ÚÌ‚­¶õ…v™ãÕˆ×ˆìˆñœŠ¬º°xï°ãµÝàß]¶ôÝQØ`âeâ…±“¶ìÍŒ¶ê¬cíŽþ”ñëñ“~“ïÉƒiðÊ´dÎYÕMîOîPä~ô‰ðIß{Ê‚‡fØ¬”Að_é‘ÓFÖ@åŠòFùEöùùZù[î€î~šd×FµJÒkù˜öt™Äöò¦¶îýL×†™ëèyý|÷{"},
            new string[]{"ei","ÚÀšGÕO"},
            new string[]{"en","ŠCW¶÷ÝìÞôŸ¸"},
            new string[]{"er","¶ù¶þ¶ûŒ©Œªpr¶ø¶úƒ¹„nÙ¦õêzåÇX¶ü›˜¶ýÇHs·¡…þÂYçíÃsš¾–é–êîïð¹ÑL†„Ù@ÙEÂx»•Ý[Ô ãsðDöÜ –˜ÞëXó’ó“ñ“øõbßƒËnÄžÞWÚ"},
            new string[]{"eng","íE—Ò"},
            new string[]{"fa","·¦·¢·¥ƒìŠ‘·¨›oˆzáÆžÛÒc·§·©°k¯V·£íÀ–ì¸Ÿ·¤áN¬m°l‚ë‘²XÁPéyóŠóŒÁU‰Ê†åz˜ìËtžž"},
            new string[]{"fan","·²„G„FþP·´’Bšï·¸éšø¢·«Ši…K·µ·º·¹–i–¯···¶·°·¯»oÅtî²ÅwñÈÜÓŒ·³èóâCØœ¹B¹D®‰±F·¬ÝGïˆï‰—¡Ÿ©J„å¾u¹ ‰“Þ¬‹Ë‹Ìá¦‡h‘Œ·®˜õìÜ”óËX­[Ä‡·±ÒTÝ™¿œ·­·ª”õÞNïcõìçxµ\ž~ž’ÞÀïx»OÏ›ú‹÷Y"},
            new string[]{"fang","ØÎ·½Úú·À·Â·Ã·¼áÝ·Á›P·Ä·»·¿èÊ °·Å•P•X­œ·¾îÕ±fÍK¼ô³”ëœE‚ˆÚÔLÚ“â[öÐó„øhô™úJ"},
            new string[]{"fei","þT·É·¢åú·ÍÜÀ·Ï·Ðáôâö–{·Ç·Ê·Î–FÃ^Ãd•h·Ñïw…Š„|·ËêŠ‚n·Ì¯X·ÆÈQ·ÈŠóŠôŠOã­ŒÐäÇœdç³ªUìé—’Žü•›ëèìqÙMì³ðòÑqòãÎN¾pôäïÐé¼UÊ„Õuöîö­•Õóõ°Cð[ü”ñIòWÏnžOòaöE™¶çšì]ïy"},
            new string[]{"fen","·Ö·Ý·ÒkŽŒ·ÔŠ}Œð·Ú·×·ØˆežÇ–Œ·Õ•Sçã·Þ·Ü–DÐv¶l¸j¼ŠÁ‰Í_Í`·ÛÓŸ·ÓÙÇ—±—rèû·Ùâp·à·ßëƒñBÉkƒfëVÊˆåªŠŽË‰žôš‘²bÄüvØkðiŸøŸþ™J–BŠ^ö÷÷÷¼Sü‹ØrÞMèMðå¯ñOüR÷a"},
            new string[]{"feng","·á·ï·ç·ë„K§·í„NŠ~ãã›h‰â·ã·î„O·â±`·èí¿ïLÒƒ·êÙº·åo›Íßô‚ª—Q¬S·éŸuˆùˆý·æÝ×ªh¥œtœ˜œ½ñTøLm“ž·ì È—÷ÚRŸ‘Å‚·ä½ ´^¯‚øPøiºAähƒtà•Ùˆ¿pÖS™lØSæ‘çQÛºÌtŒ›ž–ÐIìbïpüK"},
            new string[]{"fo","·ð–ˆu"},
            new string[]{"fou","ó¾·ñŠvÀÀŒ¼€ë€š†ø]"},
            new string[]{"fu","TÚâ¸¼·òŽˆ¸¸¸¥¸¶¸·üß‘Ùì¸¾¸º®i¸¦æÚŠmß»·ö¸§ÜÀÜ½Æ]¸½„_·ðÜÞÆ…·}¸®·÷ÞÔæâ@›Šç¦ç¨ˆ}…ò¸ÀŽ“Š•âö¸«–Ž«c·ô·þ¸c¸·¸°î·Ø“Ó‡³Qí‚N®w®tÐuÃi«sžÞ·úìðµy–¢–´–Á¸´ˆŽ›šTÜòÇC·ý‚a‚YøIÛ®àM¸©Ý³ÇX¸¡ŠÂ†b–óŒ Ð“Ð•íÉò¶Íb¸ªáœõÃôïÁ·û¹AÌ’áKÍkåõ¼›½E¼”¸¤”ê¬M¸¬ŸJèõ¸¨‹DŠï¸¢’ÑÝÊƒå‚¾Ùë¸±¸µÍÈƒÈi¸»‹c·ù¸³—­—ÓŸr¸­±Gïû¶½nÍ|ÔcÁJÒ„õÆâaãRãV¹[Å€öÖïOøDòÝÍ—½•½š¸¹¸£—Ú·øäæ¸¿à~êç·õ˜_¶O·JÑ}Ñ‡íh¹…¸¯»™ØfÝoÙxî\ó‘øWøqñ€ûŸ¾”òðòóÎlÄw·óÊº“áá¥‡`‹Ñ¿`üAÖDþEõHõV¼JÝ•Ý—Û~å‡å˜¯žÙŽíêöûôfÒLº…ð¥üF¸²ùfíë‘ÊÒiövù›"},
            new string[]{"ga","«VÔþæØê¸îÅf„L¼ÐÙ¤ÞÎßÈ¿§æÙáÜmôpäF¸Â¸Á‡QåmÜ…"},
            new string[]{"gai","Ø¤_„÷„øã½æ¸Äì¸ÃÚëà@ÇDŠ¡Y¸ÆÛò•|êàµ‹¸Ç®„½i½wâ}“©¸ÈÈ‘ëBÉwê®¸Å˜¢ÙWÙ^ØdÔ“[˜£æ­y"},
            new string[]{"gan","¸Éœ¸ÊåÆQ¼éÞ|›N¸Ë”êº«\¸Î°‘í·Ðr¸Ñ±Yãïç¤ÛáŠ›ÜÕ¸Ì¸Íôû¸Ïðáƒ÷‚‰äÆ¸Ò—U¹C»ˆ½CÔló_¶’„Q¸ÐÞÏŒ¼¹mŽÖøNÚs˜olä÷éÏÚß¦Œ¿™gŒÀôvº•ÚC÷ ¸ÓÚM÷hž¸"},
            new string[]{"gang","¸Ôƒé¸Õ¿¸¸Ú¸Ù¸Ü¸Ø¯I³M ±Œù¸×¸Öî¸þb„‚’âˆÕâG¸Ûþh—žŸ€Ÿƒ Âˆþ“•óà˜À“¾Vä“æsí°‘Þ‘ß"},
            new string[]{"gao","‰ù¸æ…Ìê½Û¬„ÆÚ¾zµ†¸Þ¸á¸ßó{°wµ‡Ç¶Žï¯ÅV¹l¸ãçÉéÀéÂ˜‚Øº•±¶J¸àÕaä†¸å·X¸ä˜°™R¸â¿c¸ÝÁoÞ»Ë›™æ€ðpíz™²úküŽú"},
            new string[]{"ge","¸ö¸êØîg…¶Ž| ç’MæüÛÙºÏ¸÷…Ï‘áÃI¸í ³„ýà¿©’š¸óð›ÆŒò´¸ïØª”š¸ñ¸ì¸çÛÁ†{‚€¸õ¸ëíÑñËô´¸ÇÍ¸òÍxÅZÑ\¸î¸ô¸ð¸éÜªœèœðàÃ†ñÓkãtéxéw¹wëõ˜†‘ë¸èë¡ïÓª˜ì‘÷ÀíkøwÝ‘ÖYÖg”RøõsíuækæŠ™ ÞPíRòZ"},
            new string[]{"gei","¸ø½o"},
            new string[]{"gen","Ø¨ƒôÞÝ¢ßç¸ù“^“j¸ú"},
            new string[]{"geng","¸ü„j¸ýàQ®u¹¢¸ûÇcßìy’ª›Êç®¹¡È@¹£—ÔˆíâÙŸ‰½c½Ž•œûf¿K¾Ùsöáói¾±õ†ùˆ¸þ"},
            new string[]{"gong","¹¤¹­ÞÃ…š¹«¹¦¹®¹²ºì¹±–r¹¥¹¯þ^þg¼k¼nëÅ¹©ï¹°¹¬Œm†yò¼çî’–í¹§Ø•¹ª¹¨…@†ßŽ³ã‰bÜpÝ\ö¡ŸË‘E´b…Cì–ó•ÓyýŠ¸Óý"},
            new string[]{"gou","¹´¾ä¹µØþÆa¹¶Ú¸¹·á¸ˆx¹º¹¹èÛ«v¹³ÂTÂV¹¸Š¥ƒÚ¯YÐ¹»‰òÂU†ŽÍmóÑÔ_ØxâhçÃã^ëgÔæÅ“kœÏåÜì°Ÿµêí˜‹“ÂÑ¾—óôÙÓMº÷¸íx"},
            new string[]{"gu","‰à¹ÅÚ¬¹À–qãé¹È¹É¹Á›}¹Ì¹Â¿àÆ‚¹¾ßÉ¹Ãêô–¾¹Êéï¹Çð³îÜÁBî¹¼Ö¼Ò†f†gƒóßE‚ïÝÔ¹½†˜áÄˆØ›üêöèôòÁ¹Æ¸šÃ™µðÀõý¹¼Ôbôþ—›ëŒ½ð Éu¹ÍÝLÝM¹Äü‰˜€â’âðó¶™ì±ïÀ÷½°–¹¿¹‡˜bƒlØÅ‹²ºH·Y¿S¼Möñ¹ËÁløõYådË[™OžJðk°›ëûþAÝž±Wî­žköAúXî™ÐM"},
            new string[]{"gua","…³ƒÖ¹ÏÚ´ˆqßÉØÔ¹Î¹Ð¹ÒëÒèé„Ž†§—I’ìð»ÁG½\ïNšOŸ°ÔŸÚo¹Ó¾ ¹Ñ„œÛ|ïWøŽòm"},
            new string[]{"guai","‰ø…¨¹Ô¹Õ¹Ös–Ê–¡ÁL“¹y"},
            new string[]{"guan","…jš¯O¹Ø¹Û´®ÂÚ¹Ù¹á›Œ¹ÚñæÒ‹ÙÄÝ¸¹ÝÞè¹ß¡äÊØžµÉF¬g¹× ¯p¹`¸A¹Üévßk“¥‚‘T¯˜ÀÝ„å]ÅoîÂð^­eëqÀ•êKÓQ÷¤ÜI°HêPæš¹à ƒ­öŠøA²•ðÙµe¹ÞÓ^÷bè…÷}ûX™õ"},
            new string[]{"guang","¹ãþ]ŽÚ¹âž»áîžÓžÕžÖ‚UÆšßÛŠ­›²ˆ¹äŸDèæ«‡ë×ÅQƒZÝ_ã VüU«EÅS"},
            new string[]{"gui","å³¹éšð¹ç¹ì”Šæ£¹êâÑØÐßžê{æ¹îØÛ¹ô•QêÁÈ²¹æ¹ñ”‹¹ëÆ—Š¹ˆ’i¹óð§¹ïÜ‰¹íŽ¢ŽëÃvµƒ«•¹ðèí…QwÒŽ¹èÑOÍŠ¹KÙF@ªg‹‚œˆ—Ë—ÎêÐ¹åÉ}ÓmÔŽàF¹òé|öÙ…‘“±‹¥²Z˜­˜²˜³‹¾“Ê„¥„£Îšý”˜æ²n­Y¶W²z™uóþõqôhëvÒ^Ïj™™°IšwŽQòo÷¬çq­„™ÍÌlíWôkè^÷Z÷i"},
            new string[]{"gun","Ð–Ø­çµ¨¹õ¹÷±šÉ€¹öLÊF¾i¾ÝöçíÞõPÙòõ…Ö"},
            new string[]{"guo","¹ý‡ë‡î¹û†FÎÐˆå‡øÇ‘ß^â£ðŸé¤ÄN¹øâu‰òä¼@¹üX‘I˜ÊbÄsðRºlÙå‘²èJßÃ¹ú‡ñ‡ó¹ùÛö›ý«àþ†©áÆÞâ—ëÄBñø¯†òåÂƒ¾[˜¡ã‡HŽ½ë½Ý{åÏXœÑx"},
            new string[]{"ha","a’CºÇ¹þÏº¼“îþœÂŠUâƒ¸ò˜Tãxì—ÒK‡æŽö•ö~÷m"},
            new string[]{"hai","º¥»¹Šo…õ¿Èº§º¢º¤ëÜº¦º£ŸQï™‰hàË†ãÜráVº¡ñ”ñ›õ°ðŽ"},
            new string[]{"han","ƒË…{Úõººº¹áí’Iê\Œåþsºµº¬ºªÎº±º¯„TÍHˆ¥‡öº·†i†cÇtº´ªRŠÎ›¿›È›Ûº­ÝÕ—ìÊº¸êÏ•~¬HòÀ—câFé\±Žº¨í™°yº«²º®º°‚þ¸Ð®]Í”¹bñUØEÎKÎL ’ÊGh‹©Þþ¶äIädäwº©•ÂŸßñüõAîhñHº¶Î‘º²º³ínîu÷ýòAënØJÖ›å«ò¥þ\ú[þ["},
            new string[]{"hang","º»ÐÐÆfÞ†¿Ôãì”ã¿»º¼°¹ç¬Ïïˆœèìçñº½¸‘ÍaØ˜ÑR¹V½Wî@ôŒñþ"},
            new string[]{"hao","]ºÅºÃê»•a¶mºÂ‚GÆ’¸hòººÄ†Sˆh¸ÞºÆœB•‰ºÁ—·ð©‚ÛàzÝïàÆª|œéÂ|Ì–ºÑºÀ•µªˆ‡s‡_ª‚•¼»°‚°€¸ä°…•ØàãÞ¶ËAË^ƒŸº¿å©°ˆº¾»DÏ–×qî—ö‚å°ž®ò«"},
            new string[]{"he","ºÌæüºÏÏÅºÎÚ­ÞàAÛÀºÇºÍ…ôŠ’uºÓºØªCPðšºÒˆ†–­êÂ±A»t¼vÐŽîÁ±B”—šBºËº¦†YºÉá—ý†ºÊÈM† ŒyŸZºÔœzºÐðÀ²»—ÙRÔZÔXªnºÈàÀ†ÛßjãØ¼ŸŒÔ†—æ´EšÎºÑØ€ãF÷…ïéuûÑÀºÏûiºÕºÖŸÀŸ¿º×îMôŸÎ˜ôçÂGºKÖy°FÛÖ‡˜ðgíHêHý[ò¢ _ e´Ò‡ÏšùŸúQúKèY°ý˜ûSìeìfìg"},
            new string[]{"hei","ü\ºÚàËºÙ¦"},
            new string[]{"hen","äºÜßçºÝ’‹ºÞºÛÔ‹ì•"},
            new string[]{"heng","ÐÐºàºãaŠ¬çñÃtžîèìºß›ê†‘Ã†ûa“Ñºá™Mø’ºâÞ¿èU"},
            new string[]{"hong","›Ú§ºë…·ŠkŒâºìÀ€ž¿›K…Ë…Æ…ÔãÈºê«Y«aºäŒfÆyãüºéˆ˜Š¼Ý¦ºå¼tÁ‡ºçÙê›`ÓÂoÁŠ¼‡•{¸f†yºæ›Äºè³{¼˜ÜŸØAéb»Žâv³…¸sœ‚œ|Ÿp“EÈˆÈ‡½“ÁØDé{ãp~ô„ì¹“ÐäUäfØFºCë”é—Ý“Þ°Þ®šô\Ùäø™‡«åé•ëŸÞZüZ"},
            new string[]{"hou","ºóºð êàCºîÆ™ááåË›•ˆ‹³@ºñºòàjŽ«È‰Ü©ºïºíØ_ö×÷ã²Tðúô×ÂF÷¿óóõ`ÂJæAðfýJö\÷c"},
            new string[]{"hu","»¥u»§‘ô‘õºõÙü…IÆSò®Ï·ÜÌŽ[àñ»¦›Z›R»¤’_ºüˆ~›~âïìæ–»¡Æ~ºÍºôá²»¢•U•Oºö•÷ºúHéõìï®@ìÃmä°ºËºø‚s…O»‡óËëaÌõúð­ÈL‰ÖŠýßü»£ŸWã±ìèœXºþâ©“‡—ü†¼‹|‰ØºùðÀçúÌ•½`Ü ¹ÍëŒ½œº÷à‚†Ø—ýìÎ“ªŒŒäïœû"},
            new string[]{"hua","»¯»ª»®‰þ…Ø»¨»°––˜»­»©æèŠ£í¹‹NÈAèë†ˆµ—É‹OâEâDîü³“®‹»«»¬“Š“ç®“‹Ã‡W¼AÓiÔ’Õj´höÙ„˜å“®±˜¥Î”Êy‹½änåkÕ–Õ ÖœüXçfÅpÀE×fò‘Ìfú†"},
            new string[]{"huai","»®»µ»³»²»´»±Ñ‘õ×Ñœ‘¯‰²‰Ä‘Ñžx™ÆÂjÌxÌ|"},
            new string[]{"huan","»Ã»¶ÃK»¹Û¼ íÛ¨»·ÇBŠJ»ÂªBä¡`»Áä½»»»½»¸žðÑ£»¼»À—hÝÈÈPåÕ†¿†¾Œ~“Qœo´»ºˆâÑ±»¾½bëf½Œ»¿Ø}Ÿ¨¬~ªxäñ¯ˆïÌ¾˜¬šZøböéà÷êa×ÈçÙß§å¾‹Öß€Øo­hæDùJõŒéIûqËà ”k¼]ÀQÞS‘×âµŒA‡ÈöZödèGêXšg÷ß­’×’ØŽóO"},
            new string[]{"huang","ŽxëÁU»Ðžê»Ä»Ê»Î•sŒrÚò»Ë‚µ»ÑŠN–M»ÆüS†Å¢“NäÒÈáååØ‹h‰Eˆð»Å»Ìé˜R»Íœê»Ï¬‰Ô…éBäêŸº˜n‰Ÿª•ÍŸì°è«»ÈÅŠóò·k¿mÖeÖWñ¥»Çó¨»Éå–ðcöüƒÆ™¤í‹æwÚ‡°Œçuòböm÷UúŠ"},
            new string[]{"hui","»ÜŒàŒá»ã‡ß»ØÏ·»Òž¾»ä»á³æ‡éì¿›xÝÚ¶hjÜîÜö»åßÔßÜÞ’ä§ä«›‘»Ó’’»æò³»ÖoÐ„ÆUí£{»ßçõ»ÚèíêÍÍYØY»âŸCŸFßDåç¶éŠî»Þ»à¯`»Ô»Ý»×ÍzÁšÁ™½}‹^“]œ“çÀëD@ê_à¹¢¡…RŸœóÙVÍ ÔœÔî¡•þ•Ÿµ˜»Ùš§—òÑ‹²N¯ÕdŽ¹‡GƒaÊ]Þ¥‡j‡vŒ“Œ“ÖˆHÂEÝx÷â»Û˜ž‘}•Áš«­_™Bî_ÖM‹Ïª›èÚËCËDË™»ÕãÄ S Zðdõtþiº_™b™m­g‘Î·x²~ÀDó³ž`‡¤ÂP×MÀLïH™®êTçi×eÌsƒªçžìuîœö™×w"},
            new string[]{"hun","ù»èÚ»»ç»ë‚[•eçõ‚“‡õ»ì›÷ãÔ»é‹GâÆ’ä¸š‰Ÿk—•Èœ†äã¬q²E»ê²O²JÓoùŒ±ÊMÕŸé’ðQÞFý@"},
            new string[]{"huo","ìá»ðß˜»ï…¿›[»òºÍå»õîØ…ü»î’»‚i»ñ¶±néX¶…¶„Ø›„Š’î†¸»öµœ»ó”üœ­ñëâ€ïÁ· tŸ´É^â·ØåŠ_‡—”N«@žCåx»ôëo»íÖf™Šïì·‚²ˆó¶ÂhÅŸòdžmß«àë¦Þ½‡É•ëÅGèZ²‘°\í_ì[§"},
            new string[]{"ji","¼¸‘’¼°¼ºŸ¼¥¼ÆM”ú¼¢„W¼Ç»÷³ß´¼ª²ŠjÜ¸¼¿„ZØÀ¼¶¼Í»øî äá§¼³»úçá¼¡ÆëÏµí¶¼É¼¦¼«›‹ˆj¼¼¼´¼ÊÜÁ¼ËÆæàBÆä¼ÁÙ¥Ø½–ˆ¼¾ò±¼‰¼oóÅ¼±°uÓ‹¸ïþL¼È¼·¸øˆ…¼Ãä©¼£…¯…uŠ ßÒßâÜùÆ–Æˆßó¼§ØÞ„ˆåìˆô¼Ì“VêéÓ›Ófï|»ý¼¹¼²®‚óÇ¼ØC¼À÷‚”ûŸd—m—ùÞá¼¨»ù¼Â¼Ä¼Åœg…hÙÊ‚ÂŽóÂ†ÀÈ—ÊDœ–ïú³¼©¼¬˜Oêåéêê«êªŽ×ê÷¼¯¯–OÅU¹UÑ_¾@Îa¶¯s»ûÚlõÒÛEôn÷äøKô‚¼­é®ê‰JáÕÝð¼»†æ¼µƒÎ„ÞëHƒ_TP˜Šôß¶Iô‡ôŠš©õÕã‚ãšöÝö«ÕH·b·IÄlÂc¾N¾f»þ¹œì´¾ƒ“Ä»üð¢çÜñ¤ûnÕ‚ÕƒÓsÜuöêÛaÛe•¸˜œ˜ÛçŽN“Ø‡\ÞªÊmËEëY„©¼½ÛÔ¼¤™v™C™W•Ì¶SåZ÷ÙÒH·]·e­^ÎŽÝ‹ÓJÙŠ¿Žºs´‰´’¿ƒ“ô·mÒQî¿ÁYå‰Ûˆ™oú”D½åËj‡™‹™›Û”ëuýTöaùH­uÂfÏlÅ·}°UùõŸöVí‡×I‘Õæ÷^ÌRÌnž†×^ð‡ìPçgçˆ÷DÞU¼®À^°^úWúaôyýVÜQíZìVèWúnö›÷CÁaÌzÌ~ÒˆýWèiÜeûAÁbÒ‰÷qóKë}"},
            new string[]{"jia","¼Ó¼×¼Û¼ÐŠA’S’zˆ]ðÙ¤¼ÑÇÑÛ£åÈ’~›váµ¼Ýä¤…­àP¼Ôš¹¼Ï¼Üçì«wëÎí¢¼ØðèÇv†k›Ñ¼Ö”Ï¼Ò¼ÙîòóÕê©Ã—kÂ_‘æõÊÑWòÌ‚íÝçË‹Tªo”ÐŽ·¼Þ†íÍÛOâ›ãe¹kÙZÄ`—Ý Ç˜–˜\˜kðýØÅ¼ÎƒrïØäeñ{î]îa¼OømôÂØjØ†¼Ú™xø”ùG¼Õæ‰û“"},
            new string[]{"jian","¼ûê§àî¼â¼þŽÔ¼é¼ä¼á¼ßÒŠ‘â½¤¼ðÇ³ƒï½¨¼çèÅ«l¼èêð¼í¼ë¼ö¼óŠ¦Š§½£›–¼ú½§¼ñ’³„‡¼æ½¡‚k‚›–ç«…óÈ¼à½¢¼ãÑI¼ì—g¼õ¼ô„ÝÑÈGÉÚÉª\Ž¥½¥œ—ˆÔ½Ø]õÂ¼ùÚ™Ôdégëe¼ê‰A½¦äÕœp“B’þŒ{È‚é¥—Êêù  ë¦ëìïµ¼ïñÐíú”ðÅ[Ù`¹a¼ò½€²R´D¼ü•©¬{¬‚—ß—äÝóÚÙÞöçÌ¼å¼øØbê¯‘ìu”Ê`ÙÔ¼÷˜c±O¼î¹{¼ýÙvôå¼G¾}ðÏ˜Ùƒ€„§ÊzÊ—„¦¾ŸÒ‰¤õÝÛ`ÕöäøZÚ{ðTä’æIÖGŸæ“ìË]„ª„«¿Vº]ÆD²{ÒMÒO™z´´–ËuåÀå¿æGðeû…íKñJ÷µùN÷œÖˆ”WžRžh´š™‘²€ÀO¿ º†µMžˆš×UÓSÓVçZùpörôCûxöxç™ç‰žžŒÏ•ÅžÞY™ÒšžèB×töúYû{í[öž×vèaèbµf»WÒ}Ì‚‡Øè{û|ídè~èƒ"},
            new string[]{"jiang","…G½³½²x½­‰á½µÜü½±½«ä®ç­½ª½°½¬®{ôøŒ¢‰Ç¿‚×½¯Èw‰D½{®–½´ŠXŠ\ÊY„ß“°@Áž{ª„Î…“ÀÚ˜ª½©ËK‰¬çÖêññðÄv¼TÏQánÖv÷šš™™^áuôÝíä®Ÿ½®ÀPÖ˜îŽ™º÷Fí\"},
            new string[]{"jiao","½ÐÜ´½»…ÓÅT½Ç½¼½ÄÙ®ÜúÆ›½¾½¿æ¯½Æjá½¾õ½ÈÞØ’›½½½ÊÓŠ¸‹½º«„’¹Ð£½Î½Ïžì•w½Ì”œð¨½Å½Â½Ã·•½ÑòÔ¹R½g½¹½·½ÁäÐœòë¸Ÿ”„à½ËÙ]Ý^Ä_õÓÛ]ãqïœöÞÌ—½ÍÙÕƒe†ý†û‡U•¯”Ò]“¼“×“è²‰‘xàÝƒ‚„¤þõŽBÙ‹É½¶ÄzÄ‰þ™‹Ðáèª—‡„‘¢½É”º”¼•ÝõoøŸ­d³CðÔ½¸·pÞBºŠÏfÏt°‰ËŠõ´×KÞIÀUÚŠç€×_‹ù½À°òœú„úŒ÷Rž«”‡á†ý™"},
            new string[]{"jie","Úà…mæÝŒ¨N½é½ÚÚ¦¼Û½×„g½Ù½æ„f…ÃŽà½ä–tŒîŒô•MðÜš²«d¯CÐw½ìŒÃ½Ü½ãÚµ„Â„o½ëÞ—ªE½áÞ×½àÐ|½Ô³V½ê½ç®vÓ“ò»…½Ûèî—A¼ÒÇ}‚Œ½èÙÊ‚ÍÇëAæ¼‹dÈ†‡›½Ó½Ý’ù’÷ÑK¯^½ÕÃ½e½YÍ„½Òœœ—¸ˆêˆûàµà®†Ö‹m‹}‹‘‚ÜŸ®¿¬˜H—ô˜PœïË“ƒÍÍŽ¹¬p½ÞÔ‘½âÓnïã]÷ºöÚìŒÕ]íÙ Ï·M½ß˜m½ØÉ•“øéQÑ›ÎfÕmì“îRôÛdŽÑ½åŽY”O™wõ^æYæOµ@°XÖŠò¡½ÖŽ^ÏÏ˜ù™ôÉÐV"},
            new string[]{"jin","½í½ñ—½ö½ïîÄµ„³¾¡æ¡½ø½ü¾¢‚Bƒ»–‡Úá½ðáŽÓb³\ñÆ½ò›»ñæŽ„„ÅÝ£ÇM²›«ƒ½ú•x½þ½ýêá½ô¼ŽÝÀµ‰®¬QÇžßM†‚¬nˆüŸ¥½îâY½û½ù“|çÆœÃ½õŒƒ½÷…ƒHÉ“‹¦âËâÛ±M¬’W‰ƒ¾oüTûvšèª­\êîéÈšV„Bƒqƒàää¿Nå\Ùø‰½‡ž‹âË| a­nÖ”½óÓPð~ÚBý„"},
            new string[]{"jing","¾®SÚå¾¢ØÙŽyŒcŠn›GˆgˆiˆlãþåÉåò¾»¾¥¾©¾¶›HëÂ¾­·¶pëÖŠ¾£ÇG„q‚\Šø›·›Ü†ÞŸ‚ŠƒôÇo½¾º¾·¾¹¸x¾ªÃ„š€ìº”ì—J—}Ý¼æºŠùªSœQ½Uö¦È…¾´¾§¾°ëæ¶“¸t¯d¾¦¾¬‚ý¾¸½›¾HÕe¾²ÛVîKÂ€ÙÓ¾¤¾³â°¾«ã½­EŽÁµìnìo¾¨îi­Z­`‘ •Ç•ß™Y¾µžsÏ‚ùXù‚ù~öLçR¾¯þ›û¸‚ü ó@¸„û—"},
            new string[]{"jiong","ØçƒÕƒ×‡åêÁåÄ›sÞ›‚C¾¼ìçˆsˆ·›ÓŸK½N¾½Ÿ ½ŸŸ¡ƒTŸâñoñ’°Ñ•îy EïGÌSÌW"},
            new string[]{"jiu","L¾Å¾ÃX`„ó…Ešð¾À¾É ¬¾Ê–`›CŠe–w¾Ä¾¿¼j¾Áð¯¼m¾ÎÅi¾ÌèÑ–Íôñ¾Â¼‘éN¾ÆèêãÎ‚wÈ\…B¾Ç¾ÈíƒŽýH¾Íà±äÐ¾¾“AøF“[¾Ë“šGÙÖ˜Í‘Wš”ðÕÅföJ÷Ýõíû…Y™ãýnúôb"},
            new string[]{"ju","¾Þ³µ„H¾äÇÒÚªÜÄßšŠŒøþ¾Ö¾Ü’]›®›t¾Ú›†¾Ð’‡¾Ó¾Ñ¾æŒþ¾×¾Ôl¾ß„ûÜÚ¹ñ•Zšj¶€îÒ ó¾ØèÛÜì‚I¾Ùê¾ç¾ãÙÆ‚˜’º’±½Û’¤šÁÁDÍi±r¾Ò³^ÐÂ`»‰Ôn¾àÇù—xÞä¾Ý›ôœHŸh¾åˆ¿ˆÏ¾Õà`†¯‡ŠÛŠè‹Jì«œ¦é§—»—ºšÆêøè¢ÄKï¸Ú ÛBôòâ ¹_Ý]ØeñÕÌ˜ö´¾â Êé·é°ÉXöÂñué…þœŒŠìÎA¾ÛñÀÛR»Å‰Õ‡Üv¾áÛgÚzä|Â‹„è„¡åðñxõXõLø~‰±åá‘§Þ“þ¸MéÙäõáÅe™h”HŒÕñïZ¾Ï÷¶ùVþF¿›Ø‹Ûžº–ùqù‰úGüŸýeõ¶ÜM™ÎÌ^Že‘Ö™ÛèL»cýAóM"},
            new string[]{"juan","ŠF„»…Û¾íŠ¤Ž†Ž™¾èä¸¾îˆ±¾êáú„Ì¾ë€èðöÁÛ²®C–KŸ]¾ìÇšÈ¦›û’Ô±’¾éÁIÑZÄC½v½²CïÃïÔ‘gÊ^„æðCÛmägäŸª™Ä–Á\æŒç×zîÃ"},
            new string[]{"jue","|æÞŒH¾ö„]¾÷šÜ½ÇÆ`{›Q¾ñ›‰«i«kçå¾õ¾ø³OÍD¯N’¢’Á¾óÍX™þèö”Ç½ÅõûÒÔE¾òáÈÚ‘ÚbÚkâfŸ}‚àØÊÒ™½~½^ãÚØãÚÜ¬œñiø`ø_‘• “¯‹ÓtÞ§Ê…àÙ‡oâ±ŸØŽ@ŽD¾ï“Þàå‘‰éÓ™@ïãÄ”¾ô™ÃÏqÏpŒÖŒØ u×HõêÜFÜBç~çÛÇ÷¬ÓX½À…ZìßžŸÓ‘Ý«P¾ðžú€²ŸØÜjè‘þ‹"},
            new string[]{"jun","¾ü¹ê¾ý…Í›J¾ù¾ûÐ‚Š®¿¤ê}¿¡ÜŠöÁ¿¥¾þðžÞÜ¿£ÇqÍSˆ­Ÿa—T•€¬Bñä¾úÈš¿¢®—œëhâxóÞ´AÎD°—°˜ƒy¹„¹‰ÙbãzÒŸã—ðK÷Œ”‘®Ÿó÷åîfòEå‹žFûŠùQùRùUõzûŽ”h”|"},
            new string[]{"ka","¿¨„JØû¿§ßÇ¿©ˆšëÌ–þÑQé^¿¦˜U¾BãläFÂˆåpæ‹ïùbõ–ù‹ö~Ìp"},
            new string[]{"kai","¿ªâéØÜ¿­žÍê]âýŠKÛîžý™üîø„ÑÝÜ„’„P†þ¿«¿®ï´é_¿¬Ýað÷‰NÌ•°ïÇÜzå|æzæbêGùbïaç˜"},
            new string[]{"kan","ÛÉþaƒÝ¿¯¿²Ù©¿´¿³ÐbÝ¨€–Ý§‚°¿±íè®¿°šKšMê¬‰{¼÷ãÛÝ|´|î«îƒêRÞR¸ƒô_ý²™"},
            new string[]{"kang","¿º…Hß’Øø¿¸¿¹‡ãýãÊ è¿»îÖ³T»~¿µâ‚é`Ü‹¢¿¶o˜±·^¿·Ü{ç_÷K"},
            new string[]{"kao","@åê¿¼”Ž¿½›Ÿ¿¾èàîíÏäD÷ŠêûŸÀWó}¿¿õwõ‘"},
            new string[]{"ke","…ž¿É¿Ë¿Çá³¿ÀÞ‘ºÇ¿Á¿Ì…\„Ä„Ë¿È¿ÍéðžÜ¿ÂQã¡ · ˜çæ¿ÆîÝðâ³`ÚŠÄ¿ÎœfˆÑæì³€òÂë´š£š¤”¨Ù¯zÁ˜ÝVç¼¿Ã¿Ê“UºÁÈdà¾“täÛƒÄâŽï¾ïýÚ Éñ½´R˜}ËP˜ÊŽP¿ÄÅ‹òò¾~ÜwÕnî§îW·iä˜÷ÁáfîwòSõ–Ð_ò¤´žµLöW¿Å"},
            new string[]{"ken","ÃG¿ÏÃ\¿Ñ¿Ò¿Ð’õ³wñÌÑyØcØ~åo‰¨‘©"},
            new string[]{"keng","êl„´¿ÔŠs¿Ó’®³n ¾ï¬³³™ÕU“¾äLå”çH"},
            new string[]{"kong","¿×¿ÕÙÅ¿ÖáÇ¿Ø›ïˆÂ£³œóíåIìùy"},
            new string[]{"kou","¿Úßµ¿Û¿ÙÜÒD„¼””íîŒtƒãâ@¿Ü·óØ„›Þ¢Êf““¸A²]²gºpúd"},
            new string[]{"ku","¿âØÚß ¿àª@³Lç«¿Ý‚VŽì¿Þ–ö¶sÜ¥ÈZÑFŸ\¿ã½fÚœÉGà·‡ý—ü¯‰¿ß÷¼¿áÑõpžQùŒ‡¿"},
            new string[]{"kua","¿äÙ¨†EŠ¯¿æ¿å¿èÚÅ~ÕF¿çã’óg"},
            new string[]{"kuai","Žw„SˆQ»á¿é¿ì¿ëÛ¦ßàáöä«ëÚšCþc‰K¿êØá÷Ž‰‘à”ƒ~Xªœ“ù‡ˆÒÄ’¼[”÷÷d"},
            new string[]{"kuan","¿í—pšE¿îšLŒˆŒ’¸T¸UÅC÷Åèwóy"},
            new string[]{"kuang","Ú÷¿ïÞÅæþÛÛ’[¿ñ ïû¿õ¿ö¿óD›rß„ÁÚ²Ú¿…N›¬ßÑ•pbêÜ³m¿ò±q¿ô³q½TÜ’ÜœÝA½_¿ðÙLãk¹nÝHÕEÕNäqà—ƒ—‰¿‘ÈüYù\ p•ç‘Ç·ƒµV²ŽÀkèk"},
            new string[]{"kui","¿÷¹é„l¿ùŒº¿üã¦•u¿øØ¸åÓØÑÚóàk¿þ¿ûÝÞà­à°ã´À¢óY‹À¡ÞñÀ£íŸ¿ýõÍêÒ‘èŸ—ó—õ¿úî¥´j…T‡]Ê‰¢‹Å‘|óñòññùÂÂ‘¸QÄ„Ödî`š•ËwÌåžæKðr™œºˆÂ˜Û“îòjêNð»Aþ}ÌwÙçŽhŽuÌ€ áÜi"},
            new string[]{"kun","À§À¥À¤À¦›ÙãÍã§•‚µŒª^‹Š‰×ˆÒˆÜÇ—yÑTÑX¶‘±—ó‚çûŸj³‰Ú÷Õûd½™¶Ÿï¿ÑhÑ‚ÎJóˆ„Ÿã­@õ«é€éöïåKòOù{öHúA"},
            new string[]{"kuo","À©’ˆÀ¨’•èéòÒ¹QÈuÈvÀ«ÀªîSó–ípíAéŸ”UžNìHíTôU"},
            new string[]{"la","„Lê¹À¬À­ØÝ–¬íÇÀ²Ç‰ÁÂä“XÀ®œ¼À°“Y—ï˜U“yðøÀ¯Î`ÜrÀ±ÞhÎ|´rÄ—ºyíBõuñ®Ëˆåå”jÅD môFö_ö~­†éJ™Ê‡ÄÏžèn"},
            new string[]{"lai","À´í‚g‚|À³áâäµáÁà[ˆêãœZÆßFŽò†‹‹@ª[ÈRÈZ—…Üm¹X—®¬[ïªíù²AÀµ¹sÙ}ÙlÙ‡ånîmäþîsòQñ®ù`ù„öDüHô¥ž|žÌD°]Òs»["},
            new string[]{"lan","À¼á°À¹ÀÃÀÀÀ¸›Ç°À·¹ÈŸÀ¿ÀÂÀ»é­ÀÄÀ¶À¾äíî½ñÜÀ½áYÀÁƒ‹ìµÀºÓE‰° A ] LïçÒ[Ë{‡•‘¾žEê@ f”G­sÒh×E‘Ð‹öŽÓÌmÌk‹û”rž‘ž™»@ÀaÓ[”Ì­Š™Ú €Òw»_ž±ŒG‡Ûž°”ˆ×Ž ˆ™íÜ_è|ïC™ì ŠÒ€¼hÀ|è”íe"},
            new string[]{"lang","„ÉÀÉàOÝ¹ÀÇ~ÀËˆ°ãÏšDÀÊ–JÀÅ‹™ŸR—OÉvÀÈà¥”ÉýœÀÆ¬˜ïüï¶³„Í™àHÅ…¹^‰iÝõÉ‡˜¸ÜqÕLòëäZéæƒ–T"},
            new string[]{"lao","ÂSÀÏÀÎÀÍ„ºÀÐ†KÇNÀÑªJÂç`·ÀÓÀÌÀÔ›Ð«™èááÀßë†[³zîîï©ðì»”éj„ÚÂä†ëñìÀÒã™ƒX‹ª÷â²‡ZÁÊ³“Æ‘Üx™Q‘Ž–U°A´‹Ïoõ²ÂgºŒÞLÜ~ç„î‘ó€"},
            new string[]{"le","ÁËêbØìÆIß·ÀÖ’Ašíà–Y«WÀßãî¸…›¹ð›³iÀÕ˜S˜VàÏðEí‰˜·º{÷¦ö˜÷w"},
            new string[]{"lei","…ŸÀßñçÀáÚ³›¤ÀÝÀà›æœIÀÕÀÛ½t‰C‚ñÕCÀ×õªãàÏæÐÉ äðçÐ˜ÃÊuîLî[ÀÚ®š´åGÀÙÀÞÀÜéÛ¿w´ °NÀØ‰¾”bË‰²™¦™§­zîµWµXÏœÀœÀhÀnèDÙúž˜ÌrÌ{ÌqÞ[×|ìYèhƒ±‰Í¶aÌ…ïK™ïèˆûPÀ}ýF"},
            new string[]{"leng","Àä‚’†}ˆÙÜ¨ã¶ÀâÀã¶ ±œ´GÛkËJ "},
            new string[]{"li","Á¦ÀúÀ÷ŒÞÀñÁ¢–^Àô„^ÀöÀû…«ÜÂß¿ÀøÁ¤ÀîÛÞÀïþGÁ¥èÀðÝìå›lŒüÆnÀýÙµÙ³ÀþÛªÀåÀóÆbhðßèÝ–ÐéöÚ\³PíÂÀùØªÀë—Àõ–Û¶w«†qÀêáû›ã›ËÁ¨†oåÎæêæ²ŠÚÀòÝ°ÇV„{Ç—‹Kà¦Àí¬P“ÃšÀæ—~ŸÀçòÃÍjôÏÁ£óÒÍð¿Ñeö¨õÈîº À—ˆ—˜Á¡ï®à¬ƒú…Àü…“„˜ÝñÉWÉTÀìäà®M²@±ŸçÊ‰Wü“…ãWôƒòÛ¹]ØN»šÅƒøEýŸÂˆ¾FÎGØ‚ŒV˜»¸{±Lšs•·Á§¬—æË…–Êk ÓöâÀðä‡åpä‚Î€ÑŸ¼HÀèøt¿rÀéóöî¾äœë_•Ñšvå¢„îŽ_žW¶Y°O™‚ë`Ö‚ár´•Ï[ùCóœÏ‹áëxõŽõ”õ•™ª Ø”^¶]•å iß†‡­Þ¼ƒ¢Ëž‡³žr­|­€”i”Á‰È™µöPç\Ï~µZûùv÷óÑYµ[Ï ¼cõ·™À­‰ s°±X°[ž¦‡ÎÌyƒ«áB÷¯ó»ÐGµ`û•úbÓ€Þ]ÜV‡ÑßŠcŒC”ƒ™æ„°×ègÞ^»hìZ÷k”‰·ˆ­–™ð÷uÀ{ìcóP÷~ûZ "},
            new string[]{"lia","Á©"},
            new string[]{"lian","Á¬ÞÆÁ±Á·Á¯Á¶ÁµÁ°ßBÁ«éçÁ³Á²çö—†Á´ñÍñÏÁªˆä‹tœ‹ÈjƒIŽÉ†öÁ®iœÇ‘X“¢é¬Ÿ’¬…­IŸÈ˜äòŠYÝü…UÒœöã…V„ ‡t‘z‹¼´nÑžÂŽÂ¾š¿€Î‹Â’‹Õå¥ÔïËOÂIåbå€Ö‹Ûšà˜ššÂ“™¹ R”¿šaì¡Ä˜ÒcÁ­æœæ`ºŸó¹žƒž‡Ì`Ì_ôHönö–ç »^»d‘ÙÀ~"},
            new string[]{"liang","IÁ½Á¼ƒÉÁ©‚ZÁÁÁÂÝ¹†|†]‚Š‚zÁ¹”Á¾Áº†¤’ë›öœ´†Èé£ÞcÁÀÃžÁ¿ö¦ýÝgÁ¸Á»ÑoÎWÜ®¾nõÔ˜ÅÝvÝˆÕåy÷Ëôuò@¼Z"},
            new string[]{"liao","ÁËÁÉÞÍŒ³îÉÁÆžÒÁÏá‘ÁÄŒ®à€ÁÅÞ¤ÁÎÁÈÛ‘lÁÌxÄkÁÊçÔÁÃ‘’àÚ”¶ùúå¼‹»â²ß|•Å˜÷ÁÇÄ‚­V¸N¸X¯ŸðÓÁÍ²tÏiØIÛŽ¿ºƒÙ’\Ë€Œ× réRÜGç‚ïfós vïmú"},
            new string[]{"lie","ÁÐÁÓ„ÃÙýä£Æ”Þ˜ßÖŠ²’ž’£†`›¼Ûøˆ´ÁÒŸI–ïÞæÁÔªdÍ}Â~ôóŸ­ïVŽ{þšƒ•ø•õhÁÑ«C”Y ÚõñôQ÷à÷v "},
            new string[]{"lin","…›ÁÚêtÁßÁàÁÖtÁÙƒä‡ÁÞßø…ÁÜÈHŸi•—ÁÕ»‘ÙU´@zàëOÝþƒj¹ƒôÔåàÁÝ„C«á×ª“Ôê¥ãÁ‘¬ÅâÞ[éŠÁØ­Uì¢•É®VŸû”Ý˜ðý éÝ™_î¬Á×®žÅR°R°S¿šÂLû‹ÜCÞOÌAžŠ‰ÉçlÁÛõïò•Ü\÷ë÷[ÜkÞ`"},
            new string[]{"ling","ÁîÁí„cÁæÁé ÷ÁëH¶ãöÜßßÊŠ–ˆ{àò‰ç•`–Eê²ÁážâèÚÁêÁgÁè°s¶{Áå¸nû_³gÐ‡òÈñö½@¬OÐeôáÅz¸ ŠêÁâèùœR’è’ç±ÀâÝCÔfÚšµ’ÑkÁäâÁãÉˆéqîIÊC¾cë‘Ýsñ|ûwöìøoõCë™ä™Ê™ÎŽX UëëžýhÁìÁçöNÛ¹‹øÌhýg™Ðì`á™ô ‹û™ý’ "},
            new string[]{"liu","ÁùÁõÂ½®qä¯”åÁø–Î—B«€Á÷ÁôÁ’Áðç¸—P”éï³®‘Ñ^ÁòÂµ‰gì¼Áïæò‹ˆÍÁóÉ]ÉsåÞAïvìÖÁñ¬Š¾^ÁS¬–´eÁöûmñ‡äïÖŸÞ„¢¸˜ñ™PéH­]ðÒ´z®œÏY°@ñœÛ‰öÌûˆæyðsìC‘Ëžg‹ôÁ[‡®Ë˜ëwïdçBçsïfòtôjö†úVïiúwò˜ "},
            new string[]{"lo","¿©‡Þ"},
            new string[]{"long","ÁúÂ¤ÅªÁüÜ×ãñÂ¢ÛâÂ£•oççëÊèÐ†U±€¸oíÃÁýÁûÂ¡—Yœ¬œöVð˜™ýˆñªÁþºTº\çXìNë]ƒ¥‡µŽaŽbÌdÜž{‰Å‰Æ z™É–V­‡•î”nÒt¸_µaµb²”ýýŽÃ@ØLÜ[ÐFÐH»\ÚLì_èxóGûT "},
            new string[]{"lou","ÂªÂ¦ŠäÙÍŒÍÂ§à¶œ¾ÝäáÐÂ¥ïÎðüâÊVßsIÂ©U‰v‘f‡D“§ŸÓò÷˜Ç®RÂ¨ñï²k¯›¯œÏNÂeÅ”ºtÜ}ÖŒ÷ÃçUþoíVótÂ¶úy"},
            new string[]{"lu","ÁùÂ¬ˆP®fÂ±Â½Â«Â®ôÂ¼ ãòÛäˆv–›Â¯Â²ëÍèÓéñVéûŸfê‘„ÎÅyðµÂ¸«Sôµ³té^ûuÂ¹ÇŠäËœGœOÂ°ŠáåÖÂÌÂ»µ“¬f—¶Â³â„³”Âµ±J²F¶˜Ì”ÙTÝ`öÔÂ·ƒJ„Û„—Þ¤ÊIÉLäõF“¦‰n‰oñe¹‚»œÄrÛjáXÚ€ê¤Â¾ß£˜Ì˜ÄŸÑÂààô”ô—ÂºÊ€éÖ“ïåhä›åjëª·c±R´{·tïåÏFóüøšè´Ö}Â­”]žZ‡£òJÛº˜ÞAðØÅ›šÚºŽº—çGæ”Â´ùcùn‡´]žoÌJ”m«G‰À­o™©öI™¾•ì­ˆ tÅF²’Æ@òƒÂ¶çeçœ»U»VÆAÀrÐBÀžÞ_èuèzú˜ÌózïBûR÷|üu"},
            new string[]{"luan","ÂÒÂÑæ®ÂÍÂÏèïÂÎá›Ã‡ð½ÙõÂÐyöÇùFŠaŽnŒDˆJŒ\™è•ð”ž¤ÁcÅLˆKž´Ì‰èŽ°f°gû[ "},
            new string[]{"lue","Œœ®ˆ·Däsäx "},
            new string[]{"lun","ÂØÂ×ÂÛÆ_ÂÕÂÙÂÚàðÂÖö‚ê‡÷¥Ç’ˆÀœS’à‹E‘—‹Ä@¶—´KœÓ†íÎFÂb¾]Ý†Õ“Ûiä—´ˆöM"},
            new string[]{"luo","„sãøÂÞÂåRÂçÜý¿©ÂæÙÀÀÓ› çóÞÛëáÂßÂÜâ¤†ªíÑ³Š½j¹JõÈÂäÄTé¡ÉzÔ›ÂãÂà ÎöÃÂáÂâÞûäðÜsîbñ˜ïÝñ§ÂÝùBõiõužTÓTæ ÙùÁ_ó»ÓZò…ÅIƒ¬ß‰Ì}«M‡Ó”{™åòŸ•ïúŸ»jÀz°eèŒð”‡Þ "},
            new string[]{"lv","ÂÀ…ÎÂ¿ÂÂàL‚HãÌ’ ’Çˆ‡ÂÉµ~—oÂÃÞÛÂÇïùÂÁÂÊÂÌÙÍÈ„¯ÂÅÂÆÂÈ½…ÂËéµƒEŒÒ¾G¾vñÚëöäXé‚Ä|Äy¹˜ÂÄ‘]šÑÄoÒ@¿|¿†ƒ–„íËƒ™°žV l™¬·„èróH "},
            new string[]{"lve","ÂÓÂÔï²ˆG"},
            new string[]{"m","ß¼…ÞÄ·"},
            new string[]{"ma","}ÂíÂðÂèáïŒIè¿µlÂêÄ¨ÂëéUÂìÂîßé†xñRÂé‚Ø†áªw‹ŒœÔ²K¯q¯r¬”¶M‹°˜qÂï‡OÊh ÐÄ¦´aÁRôñˆó¡ÏWÎ›õvæ‹ôKö‡úi "},
            new string[]{"mai","Û½ÂõÂò‰ÓÏÂóÂôÝ¤ÂöÃ}ÂñûœÙI„êß~‡XÊ{Ùuì@ËhìAö²ú” "},
            new string[]{"man","ŠÒŒÌŽÂñÂüÂùœºÂúà„ƒKÃ¡ÂûÊAá£Âø“¶‹ ªƒÂþMÂý‘`Ü¬çÏÂ÷ì×˜Ñ˜´Ù²mÒZïÜòý÷´Ï\¿zÖ™çNæžðz÷©î”ôNôMö ò©²–ÐU"},
            new string[]{"mang","ÚøÃ¢…¹šûÃ¦Œ´–x–n ¯Ã¥Ã¤±Z¸ˆ}……Ã£Ã§†WŠÁªKŽí›À ½ÇƒèšíËâI¯g³‰ÆŸÍ{‰ÜäÝä€òþÏ‘ñ ÌM"},
            new string[]{"mao","þYƒÓƒÐÃ«Ã®‰îÃ¬°pÆdÜâÃ©Ã¯ƒØá¹ã÷š¸‘ù–‰êóêÄ–µÃ³Ã°±gÃ­ÁEì¸ë£Ã¨œ~ÜšÒ‘áF¹FÙQˆéà|Ã±‹uÈrÉ‹šÊ—û•§è£ØãTÃªî¦ãwÃ²÷ÖàŽšÓóØˆòúÎcå^ó±Ùóí®ùš "},
            new string[]{"me","WÃ´„õ’CŽÛ‡¡‡ª‡¼"},
            new string[]{"mei","š°Ã¿Ã»›]ˆb›iÃ¶ÃµÆ€ÃÃ’{ÃÀƒñÃÁµ|–ÏÃ¼ñÇ±tÃzä¼Ý®’¯àdÃÕÃ·ÃŠ¬C¯cÃÂ±ŒÚ›ômäØœ„œŽˆõÉBâ­áÒ±Ã½ÃÄ‹Z‹‰ÃºŸ¢é¹˜M¶CÄP¬sðÌïÑÃ¾²S÷ÈíiÃ¸˜Ž‰r‹ÊÃ¹äY¹ŸÎnÁo BÛæVæ[ÃÓüeúB”uüq"},
            new string[]{"men","ÃÅÃÇÞÑÃÆãë«jîÍéTéY‚ƒÇ–’ÐìËž—È•¹­J Få{·`í¯‘¿ÌŠ"},
            new string[]{"meng","Œ´ÃÏÃ¥ö¼®mòµÛÂƒáÇmÃÈÃÍ’úÃÎœÉÃÌÃË‰ôÃÉà‘‰õÝùòìô»Î{Þ«˜ýà–ƒÊpŽÌ‘º‘¸”B«B÷åišÙÃÊ•äëüûsíæ²‰ãÂìDõ’öQô¿ó·²“ðìWìXîŸü€ûL"},
            new string[]{"mi","Ú¢Ã×ôéØÂãè›^ÃÚ›måµÁdü“ÃÖñ\ÃÙaÃÔìòµzåôßä›¦ŒsÑA»…ôÍëßÃØÃÐ—‚ÑQÒ“Ò’ÃÜâ¨œPÃÕÚ×ÈŽÉoÃÝœ}—Ò‰QÒšÔ™Ž¶ÉqÊUÊZDeà×ÃÛŸÇ˜a²[ãü†˜ÆƒçŽÈóÖiÃÑ÷çû†üOÖk÷ãÃÓ”Cð›Ëz™—Á]¶[º€ÃÒû”Ìjž…‡Ã«JéS­Œ”}‘ÛŒBž§ÞÂ †™ëá‚áƒûJáˆ"},
            new string[]{"mian","å²DšóÆPÃâãæ–uìrÃæííÃã‚aÃäÃßÃàŠåäÅ‚Á„ÒÃáÈx†»äÏ‹iÃåÒ±”ÃÞëï¾d¾’ÅXÎe¼E¾‚Æû ü@ìtüM‹î™†õ|™¡²Š²Œ²üI"},
            new string[]{"miao","„¹ÃîÃíÃçèÂ«QÃë¸kíðÃèŽø‹bß÷íµÃìç¿ÃéðÅçÑ‹·¾ˆ¾˜ºFRÃêåãù‘ "},
            new string[]{"mie","Ø¿Ãð…¸ßã”æŒPžû†_“}œçÃïËIøpŽÏžf‘Ìóú™­µTóºèf÷xÐ`"},
            new string[]{"min","ÃñÃóìƒí„bãÉÜåö¼Š“áºÃòãýˆ„B•F•Gëçäçë±a”•Ãö„Ç³RÁFÃõÃô’Ï‰¸œ¹IéhüwçÅœ¡Ç¬Y¬\¬z¯xíª•¡”°âŒ´C¾ré}ƒo˜‘‘‘O¾‡ä øsæFº‡÷ªÏŸöš "},
            new string[]{"ming","ÃûÃüâÃ÷Ãù±b›³ÜøŠ±Ú¤’ø±…–LÃúƒüàpÉq‹“ªuäé‘DÔšõ¤ã‘øQ˜iêÔî¨ÃøÓK"},
            new string[]{"miu","ÃýçÑÖ‡"},
            new string[]{"mo","CÍòÎÞ„õÄ©iÃ»ˆ\Ä­éâšzš{Š‹Ä¨Ä°ÜÔŽ’Ž”Ã°–£Œ­•bÂö°tï÷±u±‹Íà³]ÄªÇeÑQ½QáJ±‰Ø{ÍˆÉGÚÓÝë†ùÃþª…âÉæÆÄ®‰sÄ¯õöÄ¤Ä¡Ä£ã€ì…÷áüNôŽôžüaÄ¦ïÒ²a²hñ¢Ä«‹ººÙ˜í¼UÄ¥Ä¬õøÖƒÖ„‘½æÖ‹ß”V‡±ËÏ_æŸðxÄ¢ j°ZórÄ§ò‡µcÀg„¯ñò‡Ýð‘"},
            new string[]{"mou","Ä²„ÀÙ°ßècÄ³Ä±íøòÖçÑÛ_Ö\öÊüEøœ¿Š"},
            new string[]{"mu","Ä¾Øï…žÄ¸Ä¿Ä²ÄµÄ¶ ñãåÛéÄ·ÜÙÄÁÄ´žÑ®o®r®y ¸ÃkÀÑ\ÄªÇ€ë¤š»®€®îâÍ]³cë‚®ŽŸˆÄ¼—ú˜VÄ»Ž¿Ä¹‘HÄÀãaãfÛ[Ä½Ä£šÒÄº•½ëŽÅ¿}ÄÂíJ"},
             new string[]{"n","àÅ"},
            new string[]{"na","ÄÚÄÇ„MÄÅ…ÈŠ{ÄÉpàGëÇÄÆñÄÄÄ’‚ÄÈÄÏÄÃÐœ¼{¸™Ü˜Øvë~ÞàØyâcì„Éi†ò“ô›ôïÕ‡æ“"},
            new string[]{"nai","ÄËÜµÄÌÄÊ¯GÙ¦iÄÎÄÄÞ•ÄÍèÍá‚™ÝÁÄGœ‡Ø¾Ñ”Î—årŒY‹è"},
            new string[]{"nan","àîàïÄÐ–’o‚OÄÏ–¹®~Ç~ŠÉÄÑôö‹R“Dœ¯Èlßaà«Ÿ²éª•¨ëîòïÖQëy‘Ú"},
            new string[]{"nang","e“î‡°êÙÄÒÐLƒ²àìâÎß­ž²™òð–ýQ"},
            new string[]{"nao","ßÎÄÖFÄÕÛñÄÓp˜ÄÔØ«Ã—îóíÐ…DÄ×ˆßÀâ®òÍÔiémè§´LÄX‹š´Zô[“ÏŽH‘«DÏuÄž×D‰ëçt«LŽj"},
            new string[]{"ne","C„õðÚÄÇÚ«ÄÅ’fü“ÄØÄÄ±„ÔGŸˆ¿L"},
            new string[]{"nei","ÄÚƒÈÄÇšßšàÄÄŠÌÄÙÃ•ÄFðHåMõƒõ"},
            new string[]{"nen","í¥ÄÛ‹¯"},
            new string[]{"neng","ÄÜ"},
            new string[]{"ng","àÅ"},
            new string[]{"ni","ÄáŒÛšîÄãÙ£ÃÄâ’v ùÄàÛèâõƒºÆsÄØÄÝŠ…†RÄæêÇ–«Ãf»u¶vîê±zŒÉÄäà\ÄßŠöâ¥’íì»ˆÓˆÐœNÍe—´•©ÛCâ‰øMÄå˜TíþÄçîŒT‹¤ÎUÂ‰•¿ñD¿QÝrÓrÕyØƒÄöòÄÞƒŒƒ“ëW‹òËo”MáÚ™ûŒöFÂžýuÅM"},
            new string[]{"nian","…`Ø¥ÄêÄéÄî¶j†PŠ¨Äï¶|›ÝœVÛþ¶†ˆÄíÕ³éýýžöÓÅˆÕ·ÄèÄì“ÓÄëÝ‚öóõRð¤Û…ºv”fÛœöTÜT"},
            new string[]{"niang","Äð‹Ý‹úá|á„ "},
            new string[]{"niao","ÑUÄñÄòÜàëåøB‹–ÄçÊ\˜ÒôÁÑ™‹ØæÕ"},
            new string[]{"nie","Ø¿Ž‹ˆ[Æ}Úí–¨¯[Äôô«ÄùÄóÄíêŸÇŒÄö“Iœ¸”¤Ôà¿ÛWÛfÛh‡y“µÄ÷Äø˜®ÅYºQŽLåRõææ‡êE‡§Â™ÄõŒZÞÁ»H™Ç‡Ëýmò¨èXŽqÐA¼b¼f‡Ü×‘Übè‡ïDè"},
            new string[]{"nin","‡á’ŒÃ€Äú"},
            new string[]{"ning","ÄþØú‚AÆrßÌÄüÅ¡Å¢Äû‚žñ÷å¸Œ|Œ‚Œ„Œ‰ŒŽÃÄý™Fƒ‘‡“Ëf‹Þ”QªŸô™Ž²…Âœè_ôVûH "},
            new string[]{"niu","Å£ «æ¤áðÅ¤›SÅ¦âîžÈ–ƒÞÖÅ¥¼~Çyâoì"},
            new string[]{"nong","Å©ÅªÙ¯ßæ’˜Å¨’°Å§¶ŒÞrÞsƒzÊ‡â \™`Ä“¶Z·v°JÒaÀY×aáx™×ôTýP"},
            new string[]{"nou","˜‰ññ«Aõ–æe™“çÁ…"},
            new string[]{"nu","Å«Å¬ÂåóßÎæåæÛÅ­³eÇ‚æÀ¹@‚Õ“xñw "},
            new string[]{"nuan","ŠfœqŸÅ¯ð`"},
            new string[]{"nun","üQ"},
            new string[]{"nuo","ÄÇÄÈÅ²Åµ—jßSßö’ýÙÐÞï»Þù˜`·LÖZÛåŸ™D¼K‘ÂÅ³¼X·zÅ´ƒ®"},
            new string[]{"nv","Å®›\îÏ»sí¤–HâSÐZô¬ "},
            new string[]{"nve","Å±Å°"},
            new string[]{"o","C˜jÞ‘›¹Å¶³€ªe¹pøM”ñàÞ¿L·iÀq "},
            new string[]{"ou","ÇøÚ©Å»…¾âæÅ½Å·Å¹ê±Å¸Å¼ÄU‡Ia‰p‘YŸàšWÊqÄpøkñî¿J®TšªÖŽÅºËš™¯æ–út"},
            new string[]{"pa","°Çšñ°ÈŠrŽÅÁÆtÅÂèËÅÀ°qÅ¿Ž‡ÅÉÐ’°ÒÅuÅ¾’öÅÃÝâóáþx"},
            new string[]{"pai","›fÆÈÅÄßßÅÉÙ½ªTÅÅœkÅÇÅÈ—“ÅÆÝå¹uÝ‡æWêCº’º”"},
            new string[]{"pan","ãÜãÝ°âƒëÅÐ°é›cãúˆmžÎ°èÞÕÅÖ žÅÎ±e–®›ÅÑÅÏñÈ°ãÅÌÈ_·¬ÔjÛAÉgŽ´‹Šœã˜„“„îGäƒ±PÅÍÅË¿Tõç´‘žbÛ˜ó´æoíQÅÊñáè‹"},
            new string[]{"pang","ÅÒ·ÂáÝ…€ÅÓžÐÃTÅÖåÌÅÔÃp±~Å}ë„†ç‹˜äèÏ°òÄt°õó¦ÅÕÓI÷›óoý‰ý‹ö„ìQ"},
            new string[]{"pao","ÅÙÅ×’ÅÝˆƒáóŠEâÒÅØžäÅÚ ðå°’³hÅÛëãÞËÈa†ÔÝNÅÜìŽìsûÑŒûƒüBÐˆµPµ^ "},
            new string[]{"pei","¬êk éÅæCàúÅÞÅåÃSÅß‚_Š³”äì·›ÖÅãÅä«˜ÅàšÅÅâàÎÉ„ïÂÅáÑpñ]Ùrö¬õ¬äžÐ[Þ\"},
            new string[]{"pen","…Ü­›Åè†ÏÅçäÔÈ†å‡Šš\ÂM "},
            new string[]{"peng","ÆM„úÅê›€âñÅóy¸†Åé’¸‚‡ÇlÅõ’üœAÅë‹Ü¡°v—Z³yÑHÝJÅíÅï—Õ—Ä·@Åô‰X‰k“sÅî‚õ„™éoÅðÅöpŸÔ˜¨˜Õ‘uàØÅìÝ~ÝƒÛsñsåAíŠÅñºU´yÅòó—óŸó²ÏeÀeíŽùiÌXôJòuèm"},
            new string[]{"pi","Æ¥Ø§âÏÆ¤êoØò¶ÛÜÆ¨ÅúÉÚéÚüßÁ·ñÜÅÆkç¢èÁ–ŠÃYžÌÞ‘Åû’y ò øBÅ÷Åþš³ÅøîëÆ£±»ò·¶u¶yÛ¯Úð¼„Á‘”èÂ\ØuÆ¡ÛýäÄšÃ˜ÍoÍnÆ¦Æ¢ÄMŸÅý—À“FØwâtâWâ”ãY±Ù÷‰æÇ‹œî¢ñÔî¼òç·KÄm‡ãã›Õ|ó‹ñyøaô“ïÅüëRÆ§´iàèß¨Ý‰ªõBõQäšåCõùºfùd‡ÏKê¶‘šµFµGñ±æqÁ`‡º¯@Æ©êVÜ±ÅùþžÐKûGú"},
            new string[]{"pian","Æ¬‡æ±âæé±ãëÝÆ«ÚÒÒÙGÄAÆ­‹xçÂ—è˜FêúÙXÛMñÛôæÆªójñ‰õäÕ›òNò_ò]ú@"},
            new string[]{"piao","ÆÓÝ³éèÆ±Øâ„ÜƒGÒàÑæôæÎ®Æ¯çÎ‘GÆ®Æ°î©óª¿~ÂHºgËi”ôáoêQ Ü°Žïhïgôwî’òŠ÷B"},
            new string[]{"pie","Ø¯ë­ÜÖÒ”‹±Æ²•È“ÅÆ³çv"},
            new string[]{"pin","–Wêòšý«nÞÕÆ¶³WÆ´Æ·æ°æ³ŠÐØš¬Vé¯æÉÆ¸‹åËdÌO‡¹·|²‹µIÆµñPïAóDò­ "},
            new string[]{"ping","·ëÆ½Æ¹ÆÀ®jJÆºÆ»àZÆ¾…çÙ·Ž—ÇLèÒÆÁ›¯«rÃg³fÆ¿›Úæ³Ž£Æ¼œKŒÎÍgÍƒÂ†ÀÆE‰BŸvÉ‘Ž±®JÔuöÒÝZ¸z„R¹’‘kîZõG‘{ºq™q "},
            new string[]{"po","ØÏÆÓîÇÚégÆÈŒžnŒûF²´ÆÃÆÂ”’•^çê›¨†RŸBîÞÆÆ³ká•ÆÉóÍ—KÆÅœ”áNÉbãOÆÇîHÊXÛ¶‡M¹ŠñpÆÄáe·±ð«™áw‡¶çk"},
            new string[]{"pou","’g’h…ð†VŠË’½ˆ¡ÆÊÞåŠç Á¹rÙö"},
            new string[]{"pu","ÆÍë¶ê·ˆOÆËÆÓ’pžÊŽ}†RÙéê†ÆÎÆÔÆÒÆÖŸMÆÐ¸¬ÆÌ¯jÆÏÇŽÆÕ±¤äß‡þÉhÆÑƒWÆ×•®áTäÅmÅnÖE±©ª‰àÛªŽ“ä“òë«è±˜ã™k·oïäïè²rå§ÆÙõ‹õëÙŸ×VÆØÀbçhç’"},
            new string[]{"qi","ÆßØ¢ÆòØÁÆøßŒÆýÜ»ÆóÆùÞ€Æñá¨ãàšÝÆîÆë°ž«^è½³HÆãÆûÛßôáªŒóÆúÆô…ÑÜÎÆZÆä…æÆæØ½ÆÞÆü”ÅÃXÆçÆíµo…žÅ±[¯OÆâÆöÆõÙ¹…ýÜùÆà„~àVê‹†u‚ˆÆê”ÆÍTÍVÍ[ÆÜèç–Öšâ™û”çêÈ¸gÆðØMÓ™Ü™âHÚ–ÆÝ—RÆèßý†™†¢†ƒ†šÝÂÝ½ÈWä¿œD¢ˆÎç²ŠÝŠíªXŽ©ÆéæëÆï’Ý’åÃ¼©ÔœŒœÝÝ”ŒòÓÆÚÆå—¤—‰—Ž—«ÆÛì¥•’”ªç÷çù‰óì÷¼–ôoí ´J³žèŸÑwÑz‚úœëæ‘i‰xÆá“ ƒ[àÒòàÎB´\Æì®P•´˜¾_¾L¾e»žôëôìýRÛpéÊ­D¶Q‘h‘sÒK»ü‡rÞ­Æ÷ÏBí¬íÓ´w´ƒ´„÷’åWÖHñýë’ùCôtõèËsË~àœù‘¼Äš™‡™–º‘º“÷¢òUòTêMçKÀ™ùuù}ù†õšöU÷èÏ„ÌIÏ“Å »KçƒôGò€ö’«Oû˜"},
            new string[]{"qia","¿¨ˆX’‰ƒîŽ˜gÇ¡ÃmÇ¢³sñÊšÆþÝÖáMÚž“ü÷Ä"},
            new string[]{"qian","Ç§Ç·„XÇªÚäÇ¨ŠdÜ·¤½Ç¤šþˆTˆUÏËøá©Œò›F’RÜÍÇ@ÙÝ–e™÷ëÉÇ¥¸dÅO’ƒÇ³’ŠÝ¡Üç‚]Ç°q»xîÔÇ£Ç¦Ç®Ç¯škÍOò¯Ø@Ù»ã¥ŒÇ¬ŒRÇµÞç“bœ\‚¡ŠúâTÜ ¿êù’çèý—ìyâ`âj‹`È“‚ßxÇ«Ç¶†éå¹ã»É`ƒLÇ²ãQãUÇ©í©°|ûeÎS¾P˜˜påºÄdÇ¸óéäEÊg‘a‰‰‰q“ÃÇ±“ÊnßwÇ´…•ƒŽºGÕ˜ ™NºRåXÝ€Ç­å½ç×‰µ‹ìübîvæZÖt¿y™Œòcò`ùkºžçc™¥À`žK”o”p”q×lòqö‘Á{»RžôRôS»`ía"},
            new string[]{"qiang","Á†Ç¼ÇºÇÀê¨ãÞìÁ”ÖÇ¹«oÁm½«ª]†…†“ŠõÄÇ»Ç¿†óª}—¾œÙ“Œ†ÜïºòÞïÏæÍ“¬èÇ¾Ç½‰‚ŸÍ‘ê˜Œ¬š ›éÉ\Ê@ËN™Z‹Ô‰¦º[ÖmôÇÛ„ïêñß¿‹™{ ÀHÛ–æjçIÅšçjÌb"},
            new string[]{"qiao","ÇÉÇÇ¿ÇÇÈÇÎê~Ú½ÜñÇJá½ÇÍÇÄŽÇÅÇÏíÍ³~á ÇŸàbÈ¸†Ìã¸ÇÌõÎàƒà…„äƒSÚÛØäÛ^ÕV˜“ÇÃš¨ÇÂó|÷³ófîN½¶Êwƒsã¾‰Œ‰”‡a‹´ÇË“êçØŽÉ‰§Ÿ÷ŸòÇÁéÔ˜ò ÖímÇÊå æ@ÇÆ¯ ´“´™¸[¿”ÂNíIó~×SÚˆÚ‰ÜEÀR™ËË–ÜNçyèAíXî˜"},
            new string[]{"qie","ÇÐÇÒ°mÙ¤ÇÑæª…‚ÇÓ…LÆöÇÔ‰êü›­ã«›ù¸›Í‰ôòÜã»–AóæïÆºDÛoå›·lö@Â¸`"},
            new string[]{"qin","ÇÛÜËŽÜßÄ…Â’aÇßˆa•TÃQÇÕñæÇÖÇ×Ç›†wóVˆ²ÇØ«ÍZÂl¸Úc—v’ÍÈBÇ™Œ€ÞìÂÇÝ¬lï·ÇÙ‹]âsâ†ëdšJñû½ì€‹ŽÇÚäÚÒ“lÇÞàºŒ‹âÛ‘[ÕWøVÇÜ“åôûàß”Üäuòû¯éÕ‘¦àÓHôÀ‘¥ÏOîzñŸÏˆõùjžpŒ˜ÌC"},
            new string[]{"qing","þfÇì‰ðÜÜìiÇà®_Ç×ÇáÇâÇëƒõ„…àWÇä„ÍÙ»Çã†¦Œx’ášäš„Çéˆ½àõŽöÇåœ[³|š í•ÝX—³ÇèÇçÈƒA•¦³ òßÝpìmôìóäÇêF˜½“÷ÕˆN‘cíàöëäþƒéÑÇæžDƒ óÀö¥™”™¼õ›÷ôþ‚è["},
            new string[]{"qiong","Úö…oŒ^Çîñ·Üä–÷ÄóÌ¹HÚ^òËÍ‹Å|ŸwŸzÇí±žŸ¦ŸÅõ¼öÆ¸F²`™K­Wƒ’‘wË}Ë•­‚¸\­Ž"},
            new string[]{"qiu","³ð…´ÇðÆLáì’@Çô«UÃF–_šüH¹êÇñÇóò°ÍAÇöˆwnÙ´Çï¶kÓaÓˆÓ‰Çõ|¼z†påÏÞÇi›½á–âUí–êäš‚šÂ—WÇòòÇ°“±HäÐœrœª¦ÈcåÙÛÏ‹p“zé±Ÿª½‡Íœ©ûjÓpÙgŽ€É’¾ºE­GòøÎ~äMõF÷ü·hôÜábÚ‚öúíFíGõ‰ÏbÌUôÃù”öpöqÐ@ý•÷AþIþ•"},
            new string[]{"qu","ÇøÈ¥…ÚÐçÇúÇýá«ÒÇ„`ÜÄÚ°…JêrÛ¾È¡…íEÇü’|ˆo›µÃlëÔÃa”×ìîð¶¸lÐ ÐdÇ†…^ÇþœTŸaÈ¢Á”Â^¼ ½PÇùÇûÝ@Ç÷ÔxÔsòÐ»–¹LŸŠãÖÈÚmñlÞ¡ç‘t¸y­SÎƒêïñné‰È¤Õo”·üLüCøzõ@ó”ÏJíáè³ÏgüzüDé˜È£Ú…Ü|ÓNÓUüšöÄÞ¾÷ñôðùŠÓYòŒßž› „‘óë¬™áÅJ»cö÷Oñ³™êÐRýxÜdèŠó½ûYáé"},
            new string[]{"quan","áëÈ°È® ãÈ«È¨¾íÚ¹È¯çŠIƒŠºZ›§ÜõwÈªî°È­ãªéúžï º »³ç¹ˆ»È¦Š÷†­îýÈ¬³o½hóÜœ²È›‡ü Å—¨—Ñ“‘„áÝbÓjÔ¬†ÛIãŒòé¹ˆ¾JÛmíj˜Øñ¿XÈ©êB÷™÷Üòg„ñŽköeçzýj™àÌ†ÐSïEÈ§"},
            new string[]{"que","ÉÖÈ´È²…s‚ˆ«È±ÀŽÈ¸¬jí¨ã×— È·³‚´FÈµ°”ãÚ“nÉU‰UâÈ¶´`´_Ú|‘UÈ³ Pé êIµCùoµ]"},
            new string[]{"qun","Ñd‰æ‡ïåÒŽ ŒlnÈ¹÷åÈº"},
            new string[]{"ran","ƒÑÈ½ÜÛ…ßŠ˜ÃVÐ€Ð…È¾«zÐ™ÍcòÅ‹vÈ»ó†÷×‡YÈ¼™L¿‘"},
            new string[]{"rang","ÈÃ‰´·y„ðƒ¨×jÈÀ‘ÓÌZÈÂ«KÈÁž }ìüÈ¿ð¦×ŒÜ`ôX"},
            new string[]{"rao","ÈÅÈÄÜéæ¬ÈÆèãëN‹ÆÊßv˜ïÒYÀ@”_ðˆ"},
            new string[]{"re","ÈôÈÈßöÜmÈÇŸá"},
            new string[]{"ren","ÈËØéÈÐ„UÈÉÈÊÈÏ×šØð¡ÈÎÈÒ–ZŒãáÈÌäÃM¶eâ¿ ®–kÀéíÈÑÆ\ÈÍµsñÅÜóŠž¼x¼ŒÜÓ•ÇY–ß–á—eÑG¶‰ÄHÝØ½Vâmïƒìzì~ígôïþïšã…ÕJøž"},
            new string[]{"reng","ÈÔÜµÞwÈÓµiÆeê—"},
            new string[]{"ri","aÈÕgfihóRn‡ðutr’£ÐzâJâVâ~ñ_Å–ècÌƒ"},
            new string[]{"rong","ÈßŒ]ÈÖˆcëÀÈÞÈ×ÈÙÆŽáõ–Ñš¿tÈÝ‚ÔŸV‚æ‹†áÉ“r½qÝP˜x“mÈÜÊ‹’ÈØÈÛ·ZéÅ˜s¬Œ˜Ÿ¿^·\éFÑ’òîÈÚÎšÕñŒ‹æŽV hÁsægžqÏ”•í"},
            new string[]{"rou","…œ¶bÈâŒ`Èá»€Ã…‹YÈàœnÈ|Ÿ§Ä\˜Q¬yÎjôÛÝŠõååˆ÷·íqòk­~ù’ök×k"},
            new string[]{"ru","Èëß’CÈçÈê–dÃNÈéûÈãŽšä²–ô’Èèï¨ÑMœxàr¹TÝêÉS†ä‹‡äáçÈãœÈìÎpÊ‡Èå¿dønø›Þ¸‹çŽ]àé”JÈæå¦•ãñàÈäá}îž÷pò¬"},
            new string[]{"ruan","ÈîÈíëÃÂXÜ›‚¢‹\ˆë¬}ÄQ´M¾“ÉÝ‰‰¼­wµO"},
            new string[]{"rui","ÜÇ›IèÄò¸—MÈñ®cÈðÎT¾qî£ä„äJÞ¨ÈïÊt™G…±ÀB‰ÇÌGÌH"},
            new string[]{"run","ÈòÈóécét™˜ôÄŒ"},
            new string[]{"ruo","…ªÈôàeÙ¼’µÈõ‹SŸx’Úœc—í×ÉmóèºO kö}ö”úU"},
            new string[]{"sa","Ø¦Øí’PÆjoè•È÷ìª–ÓëÛÔQâlÈøìƒñ`ëM“—Ê”ïSÈö¥Ë_ºy™¨ž¢ÌƒÜa"},
            new string[]{"sai","Ë¼šºÜmà“HÈû†ðšËÈùÈü‡TƒwàçÙÈúî|º›öw"},
            new string[]{"san","Èý‰ÐqÉ¡²ÎÈþ‚^éd‚ãë§šÉÉ¢…xôÖšÐ ÑâÌ¼R¼V¼W¿™çDçoð€ôL"},
            new string[]{"sang","É¥–øÉ£†ÊÉ¤Þú˜šíßÑ˜ærî‹òª"},
            new string[]{"sao","É¨r’ß’ûÜ£ýÉ¦œÐÉ§É©çÒðþÏA¿‰öþÔïš×ëý²„ïbòXò}ö…ó÷f"},
            new string[]{"se","É«››É¬–ÜØÄœiï¤ëšm†ÝÉªÈû“ºšoé~äC¿L®æí“ö‘­ð£¯™­i­ži·wÀNÞQ·†çmÖ ïo÷n"},
            new string[]{"sen","É­—Ø˜¦ÒI"},
            new string[]{"seng","É®ôO"},
            new string[]{"sha","É±É¼É³É´É²É°š¢‚É¯†~ªQ»}¼†Ž¨É¶†—ßþÈSÉÝï¡êý—Eðð³†ÃÏÃ“—àÄBÊeÉµÁœì¦É·˜fÁ ¹€˜×ƒƒÒ­é„ô‹öèö®éŒóšõõæ|ôÄÀ\"},
            new string[]{"shai","É«É¹É¸õ§ºYºkº•ñ»i"},
            new string[]{"shan","É½áêÉÁßÚ¨ÉÇˆZÉ¼ÜÏÉ¾„hÉÂÉ»æ©Š™µ¥îÌðÞÉÀÉºªGÕ¤–Åžè’´ê„ô®Ü‘Ó˜Ú]éWÉÈØßÃˆ¯Z²ôµ§’ïáŸ¸–õÇ‚ÞÉÆ•ìø—ÖŸšæó±˜„š“ãˆÛ·‡AƒRŽ»É¿ŸÄÉÉ‰‰Žäú¿Ó@æÓÉÃ˜è¶U”»š`ÉÅëþ´Š¿„™cÉÄÖbõŠ¿˜óµÏ€ðƒ÷­ò~×iÙ ç—þˆ"},
            new string[]{"shang","AÉÏÉËÌÀÉÐŒ¬vÛðéä‘ûÉÎÉÌç´õüÉÍ¶@‚ûÊKCgÉÊ‘^¾yÉÑÙpìØ˜¾š‘ä–ì ÏDÓxÖ…çLôlÚJèl"},
            new string[]{"shao","É×ÉÙÉÖÉÛ…pÛ¿è¼ÉÜÜæ–¶«xÐŒÇzÉÕ„ÉÚŠ¾ÉÓÉÒ½BÉÔ±Ÿ†Èp”ïô¹óâòÙ¾KÉØÝiäûÊ–ŸýÇÊó™õ}"},
            new string[]{"she","Ò¶ØÇÉèÉàÉçÕÛÙÜÉáÅhÍFÊ°…‡ÉäÉæœh›õ’ÎÉÝê^â¦ÉßÉÞÉâÔO®Œî´®Í…ÞéÉã“”äÜÉåÝf‘bÙhÙdÊJ™ì¨ísòMÏ‡‘Øž—”z÷ê™Ý"},
            new string[]{"shei","Ë­"},
            new string[]{"shen","Ê²Éêß•Œæ’JÉòzÉìÉí² »pêÉëƒßÁA²ÎÚ·ÉóŠÉöÉð–¸šá•YÉñ«|ëÏÉõ·ŒŠ·ßÓ‚L»rïòÉéÚÅ×ŸÝ·Éïv›Ø®`±m±sÃŒ”žµŠˆÞÉîÉøäÉÉôÔB¼ƒÂÔYÝØÈ—ªÄIÑ[ò×Í–¯}é©˜YÉ÷õÉ†Ô–ÁKãhôÖÊQB®eŒ¼BävÕ”îTôñ‘÷“ËM¯”Ÿö•Ö"},
            new string[]{"sheng","ÉýÊ¥ÉúêjÉù…Ö”Î–™•N õ›ˆÆê…«{Ê¤¸iÊ¡š}Éüíò•úêÉ•…ê’³Ë„äÅ‚¯ÉþÊ¢óÏÙKÉûŸ„œƒœ¤Ê£„ÙáÓ¬]Â}ãH‰˜˜|‘™Êo¿I™T¿Œå•þJÂ•Ù‹ü›ÀKù|×W"},
            new string[]{"shi","WÊ®Ê¬Ê¿â»Ê²ÊÏìêÊ¯Ê¸Ê¾ÊËÊÀFÞyÊ§Ê·ÊÐÊ¦dËÆ…bçÊ½‘÷¸bÊ±õ¹Ê¶Û~ÊÂßŸÊÆÊ¹ÊÌÊ«ÊÔ…Ú…áÊ¼Ê»ÊµŒgÊÎÊ­û\•EÊÓ–ÉïzÊ³ÊÁ–§ìÂÊÑ^êÛÊ©•gÊÇµu±cÊ´Ê°ÊÃ›¸ÊÒŒjŠ¸ÊºŒÆÊ¨ÖÅ]ƒ½ƒàÊÊÊÅÝªŽŸÛõîæóÂ±i±x•réøžø–ò¶ƒá‹½J¹EâP³×Ò•ÚÖÈžßY“JÊªœ›œ¢Œ«ÊÍÙBÖ³Êß±sŸ³±óßâ‹ââžãAãJÔ‡ÔŠÖª{œáœÒœÛ‰PÉNÝéÉPÊÈ„ÝÓlï—ÝYþ˜øOõ§ÐêßmŒÊÄãvÅkÎg¬‹˜tÎt¹•Š]ðSðOöåø[äKñ‚õZöõÊÉß}‹ÒÌÕžÕœÑ Öuåœó§ñºüœüöXº ×RáŒÒnö|öˆö‰úP»]Ò|÷tá‡"},
            new string[]{"shou","ÞÐ…§ÊÖÊØÊÕÊÙÊÜá÷ˆ–Ê×ÊÞÊÚ›ìç·ÊÛ¯lÄfÊÝ¾R‰Û‰ÞÊìô¼ª•«Fæ"},
            new string[]{"shu","Êéì¯Êõ–XŒ«Êùç£ÊøÊããðêxÊöÊåÊà–€Ê÷XÇO‚J†Cæ­Êúïø¼‚’¿Ù¿‚‚Ý±Ë¡•øÊâŸYÊáÊüŽõÊëÝÄÉDàg’æŠìÊç½RÐgÜ“ÊòÊæÊè¯EÑVÊôŒ¥Êê¬`Êî•¤ëòë¨šÌÊäÊýÞóÊð¸wÊñ½ˆÊóã_ÛSÛ\ÊþÛÓÊûÊì˜Ð”µäø©ÊßØQÝ”åfë—ÊíË\ò˜ä™]Êïõ_°PÒe­q”dËŸƒ©ž‚¼^öWùeùŽçTÒlŒÙÐOÚHÌ "},
            new string[]{"shua","Ë¢Ë£à§ÕX"},
            new string[]{"shuai","Ë§Ë¦Ž›ÂÊË¥Ë¤ó°…i"},
            new string[]{"shuan","ãÅË©éVË¨äÌÄY"},
            new string[]{"shuang","Ë«ãñË¬w‰u‘S¿YËªëpþBç`ž“óZæ×‹þò‚ûtµd™Üú{ÆCóLûU"},
            new string[]{"shui","ãßË®šìËµË­Žœ›ä›çµˆÃŸË°¶ÑcË¯Õl"},
            new string[]{"shun","Ë±Ë´í˜˜JÊŠË³˜ù²i²pË²ôB"},
            new string[]{"shuo","åùËµË¸Ë·îåË¶šF²Þ÷Ýôª“éÃ´TÕhÕf¹›æl qèp"},
            new string[]{"si","þSÛÌæùËÈË¿Ë¾ËÄËÀãáËÂËÆ¼iËÅË…›…Ùîæ¦ìëË½ËÇŒK–yæáßÐãô›q²ÞýÙ¹‚h›£ŠÙ–Ÿ–Æ µË¼Ê³ï~ÌŒÃBlð¸†x›å›—‚Æ—tóÓñêÂ]âL½zÒ–¸rË¹çÁòÏ‹wËÃ¶D”ñït—öï•Ø|ËÁâ‘â–ãjäF˜{¶LïÈÎEÁQØË„@PÊ‘Ë»‡zäù´f¶TËºäl¾Œñ†ŸùÎ‡ÊœƒæJùbï\õ•ž[ÏaÏzù‹òlçrÒzúƒýD"},
            new string[]{"song","ËÏËÎâìËÉ–…–œËË–·ËÍŠ»ËÐþeÚ¡‚‘ã¤ËÊÔA—sŽôäÁÝ¿áÂñµ³—˜BížáÔ‚öèØÕb‘Z‘¡ë™€ðmÂ–ñžËÌó þd"},
            new string[]{"sou","ÛÅ…®ƒð’Èàn‚ÏªvâÈËÑÉLÉrŽùCà²äÑì¬“–“¡àÕËÔïËî¤òôËÒ¯˜ágÞ´ËÓæ}ðtï`”\Ë’òp™¸»P"},
            new string[]{"su","«TÙíËÕÆjËß›ƒËà›«Ë×ËÙÇxä³«ŽËØ»šƒ—VóXËÞÉG‚ÑÚÕËÚ®dÔVËÖ¸@ûhöÕÃCßià¼‹•ËÜ‰OËÝœßãº˜jßpËÛÝøö¢åÄhËõÚxä_ðM´cÜw˜Â˜É‘ˆš’¿i·dóùæÖq­X˜þË‚Û‘õ‡ÌVÌKÌp™Åç“‡Õò“ú‰÷T"},
            new string[]{"suan","â¡µ{¸Œ¯iËâ¹gËãËá…W"},
            new string[]{"sui","‰å‚ÆVËêÄò³ZËäËî›Ô‚‹ÚÇ†aÝ´Ç]Ëçˆ¼íõËåËæËìßUÀŸ«î¡²BšqšrœñËé½—´âËíëSÜ‹Ó·[ŸÕÙwÕrÄŽìšìÝ™p¶X­jå¡žvåäËë·uëm¿…Ò`¿“ÀZ”ø­…ól×\çwç›Ëèí}"},
            new string[]{"sun","ËïÝ¥áøöÀËðŒOËñ¹Sâ¸ïŠªs“p“qÉpé¾˜ƒ¹Ê˜–ËVº‹æ{úZ"},
            new string[]{"suo","ËùßCËôßïÉ¯Çjæ¶’­Ë÷èøËóêýËö¬R‚é»ËøíüœàœÅ•­ËòàÊàÂ†î“™¬ËõÑ–ÎRÚt­Fºwºz¿sõ€æiææaæ•ôÈ÷_"},
            new string[]{"ta","ËûËüËý ­µkí³ÍØ‚@ãËÌ¢ªHîèõÁ»†ÈZ„›øœÍËþ‚èàªþjãBê`ßeåÝ“‚Ëúäâ˜dé½šÏ¶Näð‰‡ß“éÕwÌ¤ÑåJÌ¡‡– [™\êìŸÌ£õ]÷£íOêFùŒÜDÒk«H‡ÅêYö•ö×nÒz÷mÜc"},
            new string[]{"tai","Ì«‰ûÌ¨ƒèšùÌ­›LÛ¢ß¾ö•@Ì¬ŒLææÌ¦Ì§ˆr‡òëÄÌ¥îÑìÆžåÌ©ÅvÌªÇ õÌâöØœÌïU‘B¹xÅ_ñ~õT MƒˆÞ·‹ê”Eõv™…»F"},
            new string[]{"tan","Ì¾Ì®Ì³ìþê¼Ì¹Ì°Ì¿ZîãÌ»Û°‚„Ì¸†®ŠòÌ½ÈI´ˆÅµ¯ÅjØ¯aÌºñûœžÌ²Ì¯ƒN˜WÌµïÄãgÌ¼ÅlÌ·‡@†ú‡cÌ¶ ‰›‰ ‘…‘˜šUÕ„á]Ì±™A•Ò•Æáa‘ŸŸï‰¯å£åUît­fÌ´À—”ZË“‰ÂávØ×T×ZÀ”‚ž©°c"},
            new string[]{"tang","¹ÌÀàûˆnÌÌÌÆÌÈ‚«ÌÊÌÃï¦ã®ÌÄœ«†°ÙÎ‚ÚàoëGÉyäçÌÁÌÂ“­Ÿ¶˜y¶Kè©Ägf‡Rñí¼CéE´gÎvÌËÌÉ„¨ÌÅéÌ˜ü CËTÛ}ó¥ïÛÌÇºLÚZ¼Qó«õ±ðnÛæhæ†çMðyêOíUç|úSôÊƒ¯‘Üü‘ ‡•ò²˜è’"},
            new string[]{"tao","ÌÖ‰úß¶|Þ„üÌÓ†Gä¬ÌÎÌÐÌÒÌ×ÌÕÓ‘ìâÌÑßûÌÍ—ƒÀ‡ÌÔµŽ½dÔ|ÑiÌÏþ“†Žµ‹—˜…ÎI¬•èº¾TìŠï‘ì’á[ä•ñŠ¿_¿lÝýÖzòPðuØ»íNíw÷Ò"},
            new string[]{"te","–Y’Pìýß¯ÌØÃŽï«À’í«ÜwäˆÏcÌpç“"},
            new string[]{"tei","ß¯"},
            new string[]{"teng","ÌÛ¯\Ž¸ÌÚÌÜbëøß‚ÎŸ¿gÄ†ñÖ`ƒ£ÌÙìLòvöŒ»L»Tþ—Ì„ü’óI"},
            new string[]{"ti","Ìå‘øÌë›¢…†ÌêÜèÌÞ‚mÙÃåÑÌé’«ç°ã©ÌèŒÏ’óßP†—‚¨ÌÝŠ¹GÜnµÌæ¶”ÌàðÃÁHÌäßX‹q¨Ìá“Wœvç¾É†ÙñÓ¬v½óe´YÑ|Ñ{²VšYäRÌß¾ŸÓÊƒšzóËSÌãõ®Ú„Û‡å÷–ÖpÌç”`ó›óžùYõ{Ìâî}‡¢­ƒòfù•ù—þö[Üƒ»GÚŒúfúeów¶_ú‚"},
            new string[]{"tian","ÌìÌïƒÌµè›pŒÄãÃÌñî±®xéåÃb®ƒîä‚ÈJ†Š¤ÞÝÌíœLÌð®\ŠõáLìjÌóÓ`•‹¬_œÌî‰\“àÁãÙ±™¯t´[¬™Ìò¾gï›ÙqÓC´k¸Kå`üVø‰­kêDìpÅqúcúl"},
            new string[]{"tiao","ÆK”þÌõÙ¬ŒýGÜæÌöŽçŒiÌôfµxìö–IÃx•qµ÷—lÈVÌ÷ñ»óÔôÐ½rÂwÉ‚ÉŠÒ›ÕAÚqÌøö¶òè˜Ô”Ó¸Iäpöæì›÷Øõ‹àæxýföœ¼g"},
            new string[]{"tie","Ìû…ãGÌùÌúÍuÝÆÂzÙNâŸï”ƒcãŽ÷ÑèFò"},
            new string[]{"ting","Ìüß‹ŽØˆNÍ¡Í¢Ìýî®®k‚KÂ[›à…ˆÍ¥ŠÇÜðÍ¤Í¦ÌþŸPèè¬EÃ‰îú—HŸNÍ£ÝãæÃµòÑœs½–Í§¹jÕP—þ˜wäbéƒöªîcÎbÂŠì˜ÖFÂ—ïFüžÂŸaÂ d"},
            new string[]{"tong","ÙÚÍ¬Í®Ù¡¶±MŽäá¼dÍ³Ûí¶²âúžçÜí†LªIÍ±›ÏÍ¨žú•zÍ©íÅÍU¯]¶‚Í­±Í°Í¯Í²Í´»½y½p½Š¹cÚUÔ˜ÍªãPÉŒàÌ„çÙ×˜¿Ÿ×‘Qã~ãnï ÷‹‘qª‘äüŸüšÔ™H Õ•Ó–SÄ€·rÍ«õjÐh"},
            new string[]{"tou","ÙïÍ·Í¶Š‡¼}Í¸Íµ‚Ê‹‹U”«÷»äWî^üWæBöWÌe"},
            new string[]{"tu","ÍÁˆMÍ¹ÍÂ›B‡íƒ·ÞƒŒ_Íº¶dLîÊÍÃÍ¼ƒòÍ»„Í½Ý±ÇxÍ¾ŽêÍ¿›Þ†l’¼’ØÜ¢ÝË—^ÍÀâQ¶•¯fÈ‹ˆà“\œÂœ£‰TÉ\Ä¯…Ä]âŠ¹\õ©ÛT¬ŸáOˆDˆE¤äŒñGòBå„ùIùWùrùúhú“ýC"},
            new string[]{"tuan","ÍÅ‡âÞÒåèªlÍÄœ¨ˆCŸ™„–Ñ‰®™ˆF‘_`“»‰’˜¤Ø‡ºiî¶™ˆæ˜¼aúoú™"},
            new string[]{"tui","wß¯ÍË‚M‚QŠÑÍÆìÕÍÈ˜UÍÉÍ‘ÍÊëPƒUÉ—Œ¾ [˜úóhÜzÛƒîjîkînòDôsË”ÌL·~ÍÇÛ"},
            new string[]{"tun","ÍÍÙÛÆXâ½ÍÌ…×ˆd¶Ú÷•H›âÜ”ëàØZÍvï‚÷ƒ–NŸlÍÊ‡pôü`ë˜êÕÍÎÄ™"},
            new string[]{"tuo","Ø±êeš×™±ð˜ÍÐ’LÍÔšúÍ×Ù¢êuÍÓ–l öAÛçë…ú…ïÍÕãû›kÍØÍÏ’„Ëµ‚MèÞèØš¼ÍÒíÈ³aÐ†Ð›Çh’É’¨Óšï€›ñÈ[ÍÙâÕ½FÍÑÃ“ˆ÷ÍÖÔqõ¢õÉÚ——øšÍ´PñWñXñjóêü˜’‹µÆÇô…™Eñ„ñ…éÒõDørùDùKõyü˜ònÌEözö¾ò™»XóCüƒ"},
            new string[]{"wa","ŒÜÍßØôßœ·Š…÷„¾ÍÞÍÛÍÚÍÝ³[®|·“Íà®Bæ´‹z†œÂv·˜ÍÜ†ì†å®H®Mëð“‰œÎj¸DÄeü|Òmící€”…?"},
            new string[]{"wai","Íâ†JÍá†·áËþZ¸î“"},
            new string[]{"wan","^ÍèÍò…e…dÜ¹„\š÷æýÍêŒñ’eŠ€ñ–vÍæÍðÍä¼w¸Š±DØà‚{Ý¸†nÍì’Â’ÌŽ¦çºˆ¾ÝÒÈX¬T—iëäÃÍïÍéŠþÍñÍí•ˆØ™•Š•–ÍóÍî—µçþÈf‰GÍåÍëîµ±›îBòê¾U¾OÝnÝkÂûŸÏÍãÛlä[äjÂDåsËHÍç²oÏTæ~Ù–Ú@ž³‰Ï?"},
            new string[]{"wang","“ƒÇÍöÞÌŒ²À ÍõÀŸÍø©ÍýÍôŒ·´Œ¶ÍüÞ‚Í÷ŒµÍúÍù¸ØèºÇwÍ^ÈDã¯ÍûÍ‡•™—ŸéþÎ\¾W–RÝyÕs÷Íž_"},
            new string[]{"wei","ÎÀàíÎ¤Îª…yÎ´Î£Û×Î°Î±Î»Î­ÆYÎ¥àøÎ²Î§‡ìãÇÎ³ãíì¿çâÎ¶Î¯Æ„Íþ_eÎ¸Î·›”ä¢žéífê¦…°ä¶›¾Î¦žù’Ëæ¸ÇUÚÃÎ½ÚóÚñêžÙË‚Î‚¥Î®Ç‹á¡Î¾åÔÎ¨™—|³u³}Î©Î¬¯_ó[ó]ó\‡úÌÎœ‘Î¼œ¿œw ‘¬^áËáÍ½Î¹†Â†Ò“f“GÒÅß`@Ž®‹y‹W‹nâ«â¬ÝÚÉJÈ”È–ÎÎ¢¬|—Ü—Û˜LìÐŸ˜Ÿ£Ä^è¸•¥œÕôºðôÔ•¾SöÛÎ«‰Š´SÎVÎOàŒƒ^ÎµÉ–“ãéÊlÎoÎk´oÎ¿ŸÝ ÒŒ¬‘ÓA¾•¾“Õ†áWÛcílîQõKÖ^ð]‘£ÁWÞ±ËeŽUƒ¤ÏG VžHžSðjéå…å—÷˜ÎºÒEõdõnítï]°LÞEžw‰Ã²‚Ë—÷ìGçAí|ögöhÓWÌvÎ¡ àðŠìSÐl×~ÜZ™ÞÜ^×ˆ”Í"},
            new string[]{"wen","ÎÄÎÊØØ°Æ[ÎÇ…ÐŠp¨’^ãëÎÆÃâžÉÃW…ÝÎÅ•jÇ|ÎÃÍPÎÉ¼y†–œb«œãÓÃ‚—S‰eÎÂ“hö©½ƒ“‹œØÞdš˜X¬ÎÈ˜vÂ„ÎÁ¯‡ñmñbøYøjô•¿AÊ•Ý˜ØnÎé”ééšðw÷—ü•ÏRæ’Ýœ·gÀIíMî‚·€ö€è·ö“êZ?"},
            new string[]{"weng","ÎÍÎÌŠT„ØÝîœå‰RÎËÇ•²²\ûlÎŠÂÞ³®YæfÀš”wúOýN"},
            new string[]{"wo","¥ÎÖÎÒë¿ÅP–†ÎÔÎÎÎÐÙÁÝ«Èn‚¬†›ðœu›ó’Ó’ÜŠñŠðªi‹_ŸsíÒÎÑÎÕä×à¸á¢ßj¸CÄOÎÏ—çÎÓÎ²YÛbö»ÄŸ­xüý}"},
            new string[]{"wu","Ø£ÍöþRþ@ÎåÎÚÎÞÎðÎçÎãÎì–Y…žÎñÚãÚùÎéØõvŒäÎÛ›@›A’NÛØÎëÎ×ŒíåüâÐåÃÜÌÎßÎØÎáÎâ…Ç…Òè»–gâäâèÎï•J«bÎäí³Jì¶ÎÙ”–…tÎê‚WàNÎÜÎóÎÝ›´ä´›žÎæßAŠÃŠÓ}ê‚‚—ßíÆ•Ç`„ÕžõÎòš’¶ñ»|·—ÎàìÉêõÎîµ¬@†•„ŠÕæÄ¶æðëFàw« èžŸoðíûcì}ÕGÎíë‰ðÄ´IòÚ˜T†èŠVÎ““‰]œ×å»Ÿ½ŸÊÎè¬šTÜrðÍÕ_Õ`öÈäoÜw¹™‘“T‹³Ž‰Êƒ˜î®W­NÎ¸PøŒëœ²yýHýIõˆìFùMæuÏwò\ù^×OÌFöV÷ùúFÜRöƒú~"},
            new string[]{"xi","…[Ï°Ï¦Ùâß…cŽ|Ï«„LÎüÏ·èÎ÷Ïµ’VŒÁâ¾Ï£…kÏ¸…äàEÛ§OÎöÎôïÃZÃ[Îùñ¶±_Y– j_Ž‡Û­àS‚S‚`†AÜçðœÏ´¹Ï¯ÞÉŠÖªLä»À…ŒÊŒÈÇ‚Çbßñ†{‰ñÆÜÎþšãš@Ï¢çôÑQÏ³ÎøâMâR¼šÁ•ÚTÏ¤•„¬Nì¤—‚—NêêÏ©ŸXŸ_Ï§†ŒÝ¾äÀœlãÒáãÝßÈ}Ï²†Õ†Ô‚ÝÏ¶àq‰IŸmŸy—ÌÏ¬Îú•‘¦À°ôâÁ—ô¸â|³’Ï¡±–ôªÅbôÑÚiñÓÎýðªãbãc½”ÚVìù—áŸ›øëK…wÉtÉYÉjÐÆÏ±ÏªSdåïíÝûÙÒÎõŸÁ‰€Ï¨Ÿ¼˜~ ÌÒ ¾kØg·GòáÂˆÚvðF¯Œ´m¿JÓB˜›Ï¥‘ï‘‚Ê“Îû‡qæÒ‹Ä”•À‡Ë@„DÚôìä‘ñŸçŸè‘ƒìû•ÊéØ˜é™Sì¨š]¿]ÖLåa¸Oó£Î‰ñÞÜzü_Ï®Ø‰õèó¬´Ž´—²qÖlØl¿uØGØH ×Ï­ O‘ò‰¸Ž`ÀGÖæˆÏkÌŸ¼YÛ’ðqô]ùTõ•÷žìIÀM­tç^ç{×@õµÓ}ë^á@‡½Žd x ÞêØêSïeòwð„ò„ìU÷@ôËÓ‚Òu÷û÷^ú ÐP²—ŒÚ‡ÖÓ„è„ÜhóNÐa"},
            new string[]{"xia","BÏÂÏÅˆYÏ»ÏÀßÈáò¯KÏºÏÁ‚bêƒÏ¿žÙèÔµ„«”žþ{ÏÄªMˆ®»£ê˜¼Ù—ØBíÌÁŽÅ{³ˆéiåÚ‚ÒÝçÏÃÅrŸ”¯Ï¾è¦épïP¹d´W²LÏ½Ï¹Ê›´lÕ’Îr¿[ÚYô Ï¼óÁå’Ý ‘³‰ìæ_æ÷ïöUòhç]öyúT"},
            new string[]{"xian","¼û™ÏÉŒÝŠhÏËÏÈ…ûÏÐÁÙÏØÜÈá­üë¯–}ÏÍÏÖìì¶iÅ`ªAŠˆÏßÆxÙþ…îÏÞÏÒÏÕêˆÏÌÏÜ’¦Ï´ÏÑŠ«ôÌÃj±]±h–žÏÔšÀžó«ˆò¹æµŠ½áýs›×ˆŸ†ZÏÝê“Ý²Ç{ˆÉÏÆŠÞ‹MÍp½LÏÎÏÏÚ`Ï³èÏÚ¬F•ðï±•óÚ½mÍ€ðÂœ¶À‰ÏÛ†¥éeéf†éÁwŒ°Œ¯‰d“{ÏÓÍ˜»˜õÑõÐõ£ÏÇµ ÏÙÏ×®QÕ^¾Q‡JƒMƒgƒmƒnÏÊã•ãŠã”äTä}í„ëU‹¹‹¸“Í“È½¾€Õt¹‘Ùt·Såß‘‘œ™Z˜ó‘—ÖPá_Ý‹ÍŽMªËWðW¿håvåDå‚í†õrÞºªž‰·ØR¿ÒD°B°G²vìÞËî‡üGÐjñMŽÒžn”gÅ@ÛŸ×]µU¼`™Ì«I”s‹ü`Ì\ö±ûyû’úNÁ{ÀoÚDÜ]Òv«NÀwýEúšú‘ú’ï@í`èvž¶÷€?"},
            new string[]{"xiang","ÏçþWÜ¼Ïò­˜ÏêÏíß½µ‰âÔÏïÏìŠ¢âÃÏàÍJÏã«“–ÙÏé•}à_àlàmÏáÏóÀ‘½|í—÷ÏÏèàxŽûÝÙÈeÏæç½Ïî„âÏñÔ”ÛKÏëã}ößðAËGñ•ÚÏðÏä¾|ÄÒVó­Ï†Ïåõa‡»õœç}í‘ð‹æøÝû‘™Ö­Ïâ÷PÀvè‚÷zð“óJ"},
            new string[]{"xiao","Ð¡ž¼ž½Ð¢Ð¤èÉ„¿…ëßØ†D‚PÏ÷æç›©èÕkžñÐ£Ð§Ïþû^Ïû›ßŽéç¯ÏüŒnàUåÐ‚jÏøÌÐ¦ÔFÐ¥ÏôŠëªVáÅÏý—nŸ^”¬Ïõ³‡·›•šš¥‚åÏú¯e¯hÁ›“`½‹óã¹qÔ‰òÙšRóï‡E‡V‡CäìÕ[ÛXäNÏöÕq“ßª”‡^‘‹°~Ä…•Ô‡[Ê’–ÖjïYºS÷Ìø{ø“º}Ër°†·nÏ]ÏSŸêš^ÏùžtÏvºón”Ã™Ï”Â‡Ì‡ÆóuújþMò”ÐDš®Ì‡?"},
            new string[]{"xie","Ð´Ò¶Ð­„µÐ°”ýÄžÂÐ¹Ðºç¥Ð©…fÐ²µmÀ‹ÆõŠGlÐ¶›ªÐ®’žàžáeˆ•’¶Ð¼…lŠÀÃ|Ã~Ã{Ðµ¼œÇÙÉ‚Ä‚´Ð³ŸcÐ±¶cŸLŒÑäÍ“aÐ»Ùô‹rªn½X½uÁ–½eÑª½â¬€½’Ð¨Ðª†àƒDÐ¯ï‰fŸ»ƒæé¿éÇÄnÑ€öÙìˆÐ¬¾™Ð«ÎqŒ‘•»¿E¾ŠÛÄ‡ƒß¢“ûŒÔçÓÊÐ¸åââ³ŽOâÝÞ¯ËZ¼IÖCÐ~í…ÒCÖxÛÆ X‰êža”XË†‡¯íCíPý^ÀTÐ·Ï’Ïå¬ yÒpýaýkÀi”yŒ@ýKõó×ýš?"},
            new string[]{"xin","âàÐÄêcß”²¿Ø¶ÐÁÐ¾ÐÃŠ|ŒJžÔê¿–‚–“ÐÀþ€±^ÐÅ‚rÝ·ÜŒ¹ÔDÔMÃ’Ð¿Ÿ{âdÐÂì§ñ^ä\Q‡ŒÐ½‹×ïâÒWÐÆîˆç†Ü°ÅgöÎá…ñQ"},
            new string[]{"xing","ÐËÐÌÐÏâ¼ÐÐÐÓÐÎÚêàDéÐÒÐÕÐÔ õ›™ÐÍÐÇ†Qê€ÜþÜôè—Ê¡ÇnóU‚†ŠÈˆž›ëã¬ŠüíÊè™â]ÈŠÐÊÐÉŸ“¬wÐÈÍ²M¾mÓqã‹ãoœîät¹ž¹“Ö_ÐÑÅdõSðhòHÓw•Ûß©°‹‹ñÅBö]?"},
            new string[]{"xiong","„öÐ×ÐÖƒ´ÐÙ×›Üº×œúÐÚ›°r†MÐØÃrÔKÔwÐÛŸ‚Ÿ‡ÔžŸÃ”¸‰éÐÜÙ‚"},
            new string[]{"xiu","ÐÝÐàÐãá¶L‚cÆvâÓßÝÐÞÐßÐåžòÐä«‹Ã‘³ôÃƒ¬LËÞð¼Å^Ðâõ÷ÐáâÊœúäå€½‘Ñ…Ñ„ó…˜¼ã–­PäP¼N÷ÛÎæT‡›ø õx¿ð}æ™çVÀC÷GçnýMïq?"},
            new string[]{"xu","ÐíÓõÛ×ÐçÐñ•BÐò›TÆ^Åò…rÚ¼…éŠV›U¯Líìí¹ñã•d™øÐôä°äªÐð‚TƒÛžíÐìˆ¦šAèò•v««—ÐóÓ’š~ÐïÔSÑSÐéÌ”›”¢Ð÷ÐøŸTÛÃ„Ô‚»× œ•äÓ‹€Ðö†Ä‰Ù“TšH•ýÌ“Â{Ô[ÐõíšÐëþCíœ½¾AÔ‚Ù[• µŸ—ì‘AìãœäÐîÉ[Þ£à†s…ÐêÐæ˜¾wÂ…²W·P±Nã„Ðèôqôˆ·VÎdôÚ¾–¾{¿Hš[‡u‹ÁªÊŒËvš_õ¯ÕšÖ[çï±SÖž ^Ë…À]ôzòÀmè`ôP÷r?"},
            new string[]{"xuan","Ðþ…ºÏØÐùãùRÈ¯•R•]«tÑ¡†IÐûÑ¤ìÅŸ@…îç•tÐžÑ£ðçÜŽÐf±†—]Ðü¬I¬KÐýÚÎÝæÈk‹lÐú‰HÞïäÖËÐ½kÍ•¬ué¸˜CêÑŸœìÓÉ{ãCíÛÊR‹Ÿäö²UÎhÂA¹Ž•Ã¶Pè¯ßxÙØämÕÖXïXìœÊž‘¤ïà™e¿òCæM¿’­vÑ¢ÂQÏ²ÌTÌBæ›×X‘Ò°_ÚK"},
            new string[]{"xue","åæÑ¨”Ä…ÉˆyÑ§lÆ‹ üNŒúÏ÷í´Ð¯Tû`–ùÚÊÑ©ÉHÑ¥Úpõ½Ä}˜ÝŒW‰®àåÑ¦ŽGÞjÖoÞm²xÍ KÞGíYžy÷¨Ó{÷Lú›?"},
            new string[]{"xun","…_Ñ¶Ñµe¾Ñ²Ñ¸Ñ°Ñ±Ñ´Ñ®–hˆ_Ñ¯ùÛ¨®p¼rÑ«»çÝ¡Ü÷Ñ·Þ™áßŽ…âþä±ä­ªFá¾¿£Û÷–Õ—DÑ³«‘š½ÓÓ–ÓœŸ[ŠQ‚Å„×Ñ­Œ¤Ùã“Mš¦¶ÔƒñZ‰_ßdà‰Ñ¬ôñ¿öàÌ¶¡„ëÞ¦‡e‡xË`„ì­R @Ÿñ˜ß”ñâ´‰¶Þ¹‡  `ÄêÖÏy²†ÌQ o‰ËÀcõ¸îšèR÷S÷\žµ?"},
            new string[]{"ya","Ñ¾„²ˆLÑÀÔþñâˆRØóÑÇÒƒêÑ¹Ñá…|ÑÈß¹Ò‚Ñ½Ñ¿åÂŽÞ„á¬ ëÑº†…ƒŽâÜˆ–‘«e¯PÑ»í¼ÑÆ’¥æ«Ûë†s‚oÑ¼ÑÁ¸Žçðèâë²ŒSðéè›Ó †¡ˆºˆÛÑÂŽÑÄ‹Iªc’éÞëªmâXÂyšå—¿ÑÅˆBíý¯{¶–¸EQÊ‹ÒKøfø†ÑÃåE‰ºùsöVý\çŒècÜ…ý…"},
            new string[]{"yan","Ú¥ÑÓ‰üÑáãÆåûŠz›WÑÏÚçÜ¾’ZÓ_ÑÔÓ…Ñ×žÏ•V ²ÙðÑÙàImÑØÑÒŠ°Š¶ŠÔ…]Ù²mÑÊƒ¼ÇrÑÐÑâÑÜÑå©ÛïâûªPÑÌŸSÇ¦ÑÎêÌ«ŠÒóëÙÑäØß„‰ÑéÑçŠ×ÑÞÒÔPáDéZãÕÑËÑÖáÃÑÍ›þØÉÙÈ‚©Û±êš†«ÈTÑè¬JÑÛ³xŸgÑÉÑÚ“RÑß‘þÝÑæìÍ—¦³ŽóÛçüëç••”©ª_†Í†ÇëCäÎœ{œ»°¼ßVÑã½žÑÑÔøHäÙœÄÛ³ƒB“CšP³šî»Ñs—â—ã—ðŸŸ‰c‰†ŸÌÑŠ´N•¶…’ÊBÑÝvÝæÌþŸõ¦áZÎi÷Ê÷Ðøe‹ÇöÚÝ˜Ü‘îÑàØÍŸð‡{…—éŽéÜyôeÌšÖV¿tø‘‡™‹ééÜ‘Ã•àºc‘±™•àüdòVòYî†îÜ‚ÆF…˜‡²‡À‹÷Ži‰Áº™ÙžÅE®[•ê™¿ wázòzüfùžöoýdþýŒÑÕú`üküjüiƒ°ŽrŽsž¥ŒE‰ÌÚIýBî›òžðô|á€×…Óƒ÷úúŽ•óŽtŽv‡ÙµhýzÆGû}óFá‰üsž ž·ØV×—ØWûšž¹?"},
            new string[]{"yang","…nÑëÑïÑôêgÑöÖïrì¾Ñî•DšÞâóÑñè–ãóˆtÑð„½’t…óŠšŒ÷ˆ”ÑøáàÑó–³žæÃoÑê•[ÑõÑí«Œ±jÑììÈÑùí¦ê–±ˆ½DÑ÷ÝIòÕÔh‚ê¤§“P˜DÑòþt—îŸ¬ãZï…ë‡Î^¬„šç•ª”®û¯ƒðB˜”Ñú÷±ñÁf˜Óå}ÖUÝŒø„ø—Ákå‘ÄžY”aï^°WµSöuìRûF?"},
            new string[]{"yao","çÛØ³Ø²ßº¦Ò¢Ô¼ŒaÑýŒë ú½ÄÆw’qŒ¸š|ëÈµnèÃ–”Å±·ŽÒª–ÌÔ¿é÷ˆÜéÒ©iÒ¦Ò§‚xž÷çòÌÕÐ‰Ò¨·ñº±lÒ¤·šÔ@ï¢‚¶‚çÒ¥È™É@†º‹QáÊˆò“eÔo¹OÝUÒ¡“uœøœÈçÑü—êªráæ‹„†ÚÉ|Ò£ßb˜e˜l•¬Ñþ¬ŽŸÆïu“ÁÒïŸì‰ø^¸G¸HðÎ´tª’ŽAŽCáèÑûËaø€ðPýGé™Ö|Ö{ôíÅ—æc÷¥ËŽ dê×ï_²‡ò[Ò«ÌißˆÀfú_ýoî–öŽúr÷]×Š?"},
            new string[]{"ye","CÒ²¨ÝÒµÒ¶…½Ð°Ò¯Ò·•öÚþÒ±ˆ’wÒ¹Ò®–¥×§’À‹ÑÊí“‚œ’Å–¦ìÇêÊÒ°îôÒ´ÞÞˆ¸ÒºÚËÈ~š‡Ò¸ ”¬ˆÒ¬Ò³ñ@˜IŸ¤•¢‰¢c‡Sƒp•Ð­LŸîšSäy°‡²w•Ïà’Ò­£”@ØÌÖ‰­ŽIŽJæUÖ]ðYæE²|•â”I”L”K {µBædðvùwèHÐJìvóBûE?"},
            new string[]{"yi","UÒ»ÒÒVÒåÒÚÒÑß®oÒäÒÔØ×ÒÕÞv°¬Òé…Fêd„ù„JØîÒÇÃEñÂîÆÒÁ±ÒàÒìÆNÞ~æÛÝÒÄÒÙÎ²ÒÊÒÛŒbˆ`ÒÖ‰ÒÜÓÆiÒëÚ±ß½…ÀƒÞ„·ÌØýÒ½–s–p”ÒÓÂkÒØÐtÒ×–¶hµtÒÀÙ«î…ê…åÒèÆqåÆÞÒïâùâø@žËÒËæä›u›nâÂ ôá»[›¥›¶Ž•Ž–ÞÈÒÌêÝéóq’Þ–ÜèÞÄŽåßÞß×‚X®A«}«pÒß–¤•iÒÏôàô¯áÊ³ôýØ—Ó”¸”Ð‘Ðš™ýŽƒ‰ñ‘ü‘ý–Ø–å–õÒæíôÒÈÒÐ†jÒêÞ ÞÚã¨ˆ£Œh‚›Å›Îœjâ¢ÛüˆËÒÝÈUÈ^‚Ã„ÖàcÒÆ–š¡Ò¿ðê®®ŠÉßñ´ÒîÔTÔUØ[Ø\âNëcó`Ú˜øCÒÂârâzãiÔrÔmÙOÝWÍ‚Í~Í†ÒÎ—©—×”§”î•”àvÒÅš…Ÿ|Ò¾Ò¼©œ™Ž¯‹fÒçª~çËàÉƒÏÁx{•—à¯m²G¶BÒâ¶êÑv¸vÔ„ÕBÒÞÑ`ÛDïí›ñkãžäFÒÉòæ·FÄjðù˜]ì½„ãMÉšäô‹¡‹‹Â“ÌìÚŸÖŸÛŸéßzƒ|ƒx˜¯¯Žûkûoï×ÒãºIÕxÝ}ü]ÛpîUîVÅ’¿O­CÎœÎ•Î’ÒA·j²eûp¯–”¹•Ë£ØæÞ²àæ Déì‰©ˆI‰ß‘«‘›‹Î‹Ú¤ÛŽFŽKáÚŒ•Œ– J W•Ùšc”¾™}™jÒÜ¿ˆôèÒíºmÙŒØŠî{ø˜õlõk÷ðÁrátæ„Ù“Ö–š­ñ¯µEïî™ÒÍË‡Ë„¥çFØsÀ[ÀXÅœÏÒÃÒáìJöGù€ùù‹ùŒî‰ð†»Já{áy×g×h×bÞTÌ[ž‹‡ÒÓ~èOúgú^ús×‚Ü²Ò~ú…óAüpýtúœû@Ìˆ×”ý~"},
            new string[]{"yin","ÛÈƒÜÒýÒü‡àÓ¡ÒòŒèZÒõêfÒ÷ßÅ ìÛßÒûžô”ÕðÜáÒðÒñÒöóSä¦›Ž›ÛóˆŠØ·Òô«ò¾»ƒÓÓ—ˆ¤|–ðë³Òó‡ôáþÇZêŽÒþê›ê”‹HÒúƒø•ŸÒùÔCÑPÚ_¶†î÷Òø½s¹Ný‡âiâwï‹œšÜ§ˆøÖœ^ªZà³‹AJÛ´ëLÊaÉMµšœÞœôï‡ì‚´H‘@¯ŠÝl–@ñ¿áSö¸ãŸãyâ¹Ê_‡wš’ìÚyÕz¾ž°E‘\´€­K‘€‘ñ«ÖNñ—ë–õg™aë[‡‘‘¶þž@Ž\ë éžÏPÏr™ƒ™’‡¨Ìaö¯ý]í™Óýlþ“°a×ú"},
            new string[]{"ying","Ó­Ó¦ê°ŸÓ¢ÜãÜþÓ«Û«–ÓÓ³Ó¯³A—@«›‚ŸÝºÓ¨›Æœ€Ó©ÓªÝÓ†Ó—wÓ¤‹kÀ†áÍwÓ²–P•£Ÿ–çøÈtœ»œÁäÞÝöƒOéº‰Lëô‹”´QéA¾xó¿ÙaÓ¬ÎsÞü“²Ó§ŸÉ®OàÓäëè¬}Ó£Ó°Î„Ä{¶H¬“À”¿MÑš·fñ¨ÎžðÐøŠë›îeÙø I‡|ËpõöžL‘ª‹ëíŒêCÓ®âßÓLÖhÓ¥Ï‰ævò£®Zž]å­ž„žu”lŽcÓ±ùúDçïI×GÚAÀ›ŽgŒ[”t‹ý™Õž‰Ìc‡Â_ž¡™Ñ­‹×sµ_úLè]°`»YÀtÐN÷júˆž­ú—ûK»kûW"},
            new string[]{"yo","ÓýÓ´à¡†ÑÀ’"},
            new string[]{"yong","ÓÀÓÃð®Ó¶ÆoÓ½Ó¾Óµ[–ÔÙ¸ÓÂ„Ê–º~Ó¿ˆ¬Ó¸³lçßÓÁÓ¹ÉKà¯œ¥Ó¾³‹ÔÓ¼Óºò‰M†Þà{Ó­‚ò‹£Ü­Kã¼Ó»ú÷‘ œ“íÑÛÕ‡‡àaÛxïÞÓ·ákëtî„õ—°MçO÷«öïJbžœ÷Iúx÷Ó°b"},
            new string[]{"you","ÓÖÞÌÓÈÓÑÓÒÓ×ÓÉ®hÓÐÓÅÓÓØÕÓÊÆhJ›YØüÓÇÓÌÓÏ ûŒMJQÃUÓÍ›|ßÏÙ§ÁhÓÕ†NÞ”µvÓÄ ¶èÖå¶ŠµfàóMðàŽîr–ë¶xÓËÞœ†e›ÁÝ¯ÝµÝ¬à]‚ºßKîð—XÔIÓÆÂuòÄòÊÓÔØzöÏªqß[ÓÎœ±Œ˜Aµ™éà÷†â™áRû~ÍœÝjÕTÑ„äBñfHÊ~ë» ¨˜©ôœòøòö‘nÝ’õO÷îôíƒžà›žX‡¦‘É÷ø™¢™ÔÀlÂi"},
            new string[]{"yu","Ñ@þ’ÓëÓÚ€Óèí±ÞzÔ¦ßŽÓñÓðí²­ØñÓóÆRÓîÓØÞ}ÓõÓì’H’GæúˆSâÀó’Tãé–fì£«_«]Óàæ¥åý¹Èô§·‹ÐsÓÛÓãÓêÓôê|™öÓýì¶Ø®Óí’§êÅÙ¶‚RÆ‘ÆœÓüÓáƒÊÓï¶rÍGô¨¼uóÄáC³_îÚÚÄáüÓéŠÊŠØ‚qµ€–üÓøÔ¡ªâÅàôàöÓòˆÖ±ÓÙÓæœUœMƒÓû‚¦Óçàhêœ…PÝÒÝÎÝÇãÐÎ¾â×ÚÍ†‰ßN±E” »Šö§ô~ñSï„ó^Öà®Œ³‘³†Ô£èž²œðÁÓâÓöà¯Ó÷†³†ÉÓùŽ÷Ô¢‹VÈgÈh¬Zëé—§—™—š˜KŸ~ÓåœŸ·áÎ£ÞíÓä”ÑˆèˆïœùìÏ ¢êìÓÜ—å˜@ÓÞÓúè¤¬ršQÝ÷Éf‚øŒ†ðöðõ·CÁNÅcÅ„â•ÓÝÓþîAÕZãƒÝh¹zÓß¾sÑˆñ¾òâÎCØ¹²I¯ªz‹žƒhëTÎµ‘íOîˆìÙ‘jšuÊ É™Êvßy·UòõñÁä`Õ˜Ô¥îYëkðNö¹ø\øƒøˆø…é“Ô¤ÓDÖIå[Ø…Ûu‡‰‹äÊšìÛŸúËÄŽZ‘µše­mƒ™å“Ý›ÏL¶R´›ðÖôrõ‚ùOókû‡°K·{µHµNºháqÖ~”Ë™È”ù»Bð|òeöiìMÞX×uçŸÌPÌ]ëzòå÷ú}ýr™äú–÷N»ZûCûOý{™óôcÜ†ôdþe»nžº Œý›"},
            new string[]{"yuan","ÔªƒÒ‰íÚOß–Ü¾Ô±Ô¶ãäÔ°‡äÃOð°–zŠ†Ô·ÆŠÔº„uÔ«ë¼±\Ô¹Ø’Ðc¸ÍWíóÔ§ÛùÔ²Ô­†TÔ©ßR…ŒŒw›ðÔ¨œaœeœmœ®œYªjæÂÉA‚Ó‡ûÔ®Þò­ö½ÔµÔ¬Í›ûgÑrÜ«ˆ@ˆA—¥µžè¥ßhÉVÉd‹‹…Ô³Ô´œÆÔ¯˜g˜rÑjÑ†Ô¸øSóîñrô’¿F¾‰ÎzÎQÎmÑ“ó¢éÚà÷ËQ‡…ä‘øxüxÞ@Öwæ…îŠùtß‡‹õ™´ž”ù ò{úMüŒÁ~ü…™"},
            new string[]{"yue","Ô»ÔÂ•õ‘àÀÖë¾Ô¼Œé’`Šxµj›‡«hÔÀxßÜËµÔ¿èÝ¼sÍRÍQÜ‹îáÔÄÔÃ‚’ÕÔ¾Ú”Ô½ÔÁâ_ãX»›¹–é‡é†ºM‡‚‹íéÐŽ[Ùß³E»Cügå®Ìg ~¶^ÜS»aûNè€»lûV"},
            new string[]{"yun","ÔÈ„òÔÊÔÆÔÐêm»ÔËŠ@Ô±Šuáñ’d’l›VÜ¿ç¡‡ç–—êÀÛ©ÔÇÔÉ¶n±dã¢®sÔÎÂmÔÅ¼‹Ç\›éàiéæÔÍë…âqã³ÁÀˆëEàyÉC†½ß\‚ÖœÝÉQÉl‘CäŸ±óÞ¹o•žè¹ÄZëµÔÏñaÑŽšèŸ¾ŸÂšŒìÙ´pÔÌÊ|·Š[Î‚ä]ádÊŸºJÙ„¿Z¿a˜øÚSájðaìBíyírñNËœÌNÙšíýqýy"},
            new string[]{"za","Ž‰ÔúÔÑÔÓ›eßÆÕ¦uÞÙ›jÔÒ–ý˜TíˆãNëjåpô˜™UÒSësÅH‡Í‡ÔÅNë{"},
            new string[]{"zai","×Ð’DÔÚÔÙÔÖžÄKçÞ›’ÔÕÔ×ÔØžüÔÔáÌ‚îœ…œÖÝdáP²Pƒ„¿fÙ†"},
            new string[]{"zan","ƒ³ÔÛÞÙêÃ‚ÌŒv“S†¹ÔÝ•ºôØÙm‡k“ËÔÞ™VöÉºdƒ›à™àŸ”ežUô¢ºÛŠùaçYÙ­ÔÜè¶ç‘ç‡ƒ­áA‡Ô”€×{ž£¶`­‘ôõÒ{×“ÚŽð•"},
            new string[]{"zang","æà…MÞÊn ™ÔàÔß‰ZÔáÙ_ÙjÊiê°ñzäQ²ØÄ ÅKÚEóv™âÚN"},
            new string[]{"zao","ÔçÔí°oÔîÔæ–ÒÔéÔìßð†r—_——Ôä‚ó†×Ÿ¯Å‘VÔâÔëÔèºrÔã­b¸YËkÔïásÛ›ÔåÔêÚ‹×Y¸^è"},
            new string[]{"ze","ØÆŽÙšòÔò’k›gÔó›zÔðê¾•WÔñåÅ²àÕ¦„t’¾àýßõ†¨ô·óÐØŸ¡³’œõœÚ²žóåŽ¾‡K‹¨ÊjØÓ˜ÁštÕ‹²c°ƒ“ñºjÙ‘Ö‰´ŸÒ]Ïý`ývûB"},
            new string[]{"zei","Ôô‘åÙ\÷Œì—ÏŒöf÷e"},
            new string[]{"zen","ÔõÚÚ×P"},
            new string[]{"zeng","•û×ÛÔøï­‰ˆà‹çÕä{ÔöÔ÷ÔùŸå™Iêµ­Q´Œ³DîÀ¿•ôgÙ›ÖŸ‡×"},
            new string[]{"zha","ÔúÕ§ÔýÔþß¸…~Õ©žÁÆzÕ¦’€’sÕ¢ŠL’ŸßåÕ¨Õ£²éÕ¤×õ–¼ðäßîŒoíÄ¼’¼™òÆÍlÕ¡‚¼âªÞêÔûà©ÔüœÑÔp £él÷‡é«é¶É“’“«„žÕ¥°•÷À¯¹†Óuë˜Ï°šõWõ~ÂdámåŽ×A×Q÷þýO"},
            new string[]{"zhai","Õ¬²àÔðÔñ‰ã»yÕ­’ÆÕ«²ñÕ®”ÈíÎ¼À‚ùãSµÔÕ¯˜zÕªñ©™yýS"},
            new string[]{"zhan","Õ¼×Õ´Õ¶–Õ»Õ±ïsÕ½ì¹–î—CÕ¹Õ¾ÕµÍtÕ³ïÕ¸‚·Ç•”ØÕÀ—£Õ¿ÚjÔa¬W¬±KÕ²énÞø‘éÕ·˜^äãïQ¾`Ýu‹¶á\øÚÞËUß‡~ŽE˜ö‘ðë•Ì›ÌœÝššØšÖÕ°ûrÒfÓOÞJ”ö×d×`ðŒø@ò–Õºò ô}ûD÷gür×–"},
            new string[]{"zhang","ÕÉØë³¤ÕÌ’EÕÈÕÅÕÊÕÍÕË»wÕÇ›îŽ¤ˆÕÂÃ›ÕÆ¯oÛµÕÏƒ@ÉŸßlá¤æÑâ¯áÖÕÃÕÄq‰z‘P» Ù~ÕÁ•Àè°ŽÇÕÎ¯“ð\²d´˜ó¯çbò†û–÷J"},
            new string[]{"zhao"," ×¦ÕÙÕ×ÔÚ¯ÕÒîÈÕÐÕÓŽ‚Š„––ÕÑªDžÝˆÕÔóÉÃAá“×Åßúèþ”í³¯À’Ôtü…ãD¬ÕÖÕÕÕØÃD¹|Úw³°ñqå™•× Yõe™˜²°œÁ^"},
            new string[]{"zhe","…zÕâÕÛšyÕß³K³Y»qèÏÍEÐŸˆ³Õãß@ÕÜ»„†£œJéü×Å•†•‡†ÕÝ†´Ô€ÚØòØñÒÕàÕáÕÚ†øß¡Ým‹«ô÷íÝÝtÛzäO˜ÎðÑÕÞñÞæNó§ÏVÏU‡¬Ö†Ö•õ„ÞH×yúpÒx×„"},
            new string[]{"zhei","Õâ"},
            new string[]{"zhen","ÕóÛÚÕêÀƒÕïÕëÕí®lÕì’r’™ä¥éô‚Eê‡Ö¡Šªð²•_ëÓÕä«‚Ø‘á˜ëÞìõèå–ÚÕèÕî¼…î³ÐÕæ±p±wŒÇŽžÕñêâ‚É±‡½GÂráIÝFÒ˜Ô\Ñ]ÈœŽ¬ŒzßZ‰`“LœäÚ“ŽçÇ‹ª€ÝèÉR½„é©˜EµÕåÕçâœÕgÙc¬‘¶Gé»´Uš‹›ð¡Õòóðä‹Õðñ}øcåg˜ç¿b¿jÕéØËmæPÝŸææ‚Þt»EôIüm÷y"},
            new string[]{"zheng","¶¡šéÕýÕùÍÖ¤’cÕú ŽÚºÖ£Õ÷Š’¼lÕøá¿žÚÕþˆÁÕõÕüŸAÃwîÛ±kÖ¢Õöï£Ât’ð’ê”˜ªb‹o“@Ž­þóÝÔ^ÕŠÛtã`± ìk•“‰^ÑÕôàñ¹~åP“ÕÕûºPöëô@øg×C°Yçdþ"},
            new string[]{"zhi","Ö®âºÊÏÖ§Ö¹Ø´Ö»Ö­Ö´ên„¶Ö¥ÆWÖ¼ÖÁõôÖ¾âåÊ¶Ö½ŽÖ·ˆ^ÜÆŽŽÊ’W’XÖ¨›E›b›DÖÎ›‚Š‰’nÖ¶Û¤…„ÖÆÖÄàùÞŒÆ‡ˆpˆ€Ö¯ÖÊÖ¦ÖËìíÖ«Ö±Öª³UÐ}ÐëÕÃe¶q¶oìóµwèÙè×èÎ–»éòdÖÅŽæŽèÚì‚fÖ¸’”Š©åë›œ›±œ]ŠÍÖµ‚u‚ŽÆ¼ªOéùêÞèä–ñµ…Ö¿•yÖÈÖ¬šlÐ—Âp¼ˆ¯W¯Uá™ÖÂëbÁ“Ø ÓdÔJÖºÖÌ¯bèœðº®‡ÖÏ¼•Ö°¶ˆ—„—dªavÛúˆÌÃ‚À„ŒÖÀœFÖÍæï‚Ðª—ÐÖ²Ö³ÖÇåé‘çµ•ôêòÎ ÃðëõÅÝTÅ]íéïô¹eÝeÛNõ¥é@ÖÃ½¶A¶žÖÉ­•DŒ…“w“ˆœíœþ†ÞýÕ˜u‰y‰~Ö©¯F¯€ÒžÕIã‡Ñuñ\ñcøTÙ|ö£äkõÜõÙ‘e‘pëù·W“´ZŸÜáçŽÃ“¯‹Àžž\”Të\”ò­MÄˆ˜à¿@øvñ‹øÐ˜¿{ÏH™±‘Á·a„¬ƒœ”`”SËŒ‘ÆÏdÂš¿—Û—Û•Ù—ÓzÜÒjöSòs™£­}ü~µYØTÜUòòŽÌuúvÜWèeØU"},
            new string[]{"zhong","ÖÐÖÚÖÙ«›O³„dŠqŠtâì ðžÆµrÖÒÖ×›wÖÕÆ ÖÖÖÓ–°ÐxÐ{ÖÑÖØô±Í\Ú£‚£±Š½KÖÔ¹Wâ{ˆú†ÁŒ»‹gŸŽÄ[šp‰VŽºÊWïñ·N¯~Î@äV×ÖAõàÎ ø‚ó®æRü™Ð\Û çŠ»b"},
            new string[]{"zhou","ÖÝæûÖÛÖâÆÖßúÖÜÖä…âç§ÖãÖææ¨¯JëÐ×£¼qÖÞÖážëÖç†BƒÙ×žÝ§þVµ÷àXžöÞbôü«‰Öå³B»‹Åûbô¶•ƒœ@‹BßLÈFßúþ`†µÈ’•ŽÚQÝSÔkÖà²H®LÝcã{íØ¹þUÙkƒu°™ñtÝq¿Uñ™ë“‡€þ_ù@‡œÖèÖaôí±TòLô¦öB»Q»N×póE"},
            new string[]{"zhu","Ø¼Ö÷ÄþÊõÖìØùÖñ×¡ÐÆ^ÜÑÖú„¸À‚ˆ|Öü×¤èÌŒeÞŽÖô’}×¢ÙªÖïÛ¥óÃ¸‰×£ÜïÇAä¨èÖÖù–ÇšŸ‰ÔìÄžÛÖòÖé¸m±vÖêÖðÇdê•Öîµ‚ðæÁC·”³d³p¸˜ÑNÖû¼Ÿ½Aô¶ÖøßIÊüŽªä¾¶‹îùÖí­ÊôÖóŸ—ÖýþqÚŸ½ZÖëÖþÙAÝOÔ}Ô]ÕD¹hñÒðñÛHãLïŒñ[ÉÊxäóéÆ‰£ãÎwôãóçÖTØiäŠñvéÍ˜ÖîÖö Gñ–÷æû„ºBºaºZº|ø–õf TÖõžzË ü}ö^ÏŽ™Á™½õî÷EÐEèT„±ž¯‡ÚŒFúž™î•ô ‰”á²šÐWè“"},
            new string[]{"zhua","×¦×¥ÎÎ“ë™tÄºœó˜"},
            new string[]{"zhuai","×§×ªÛJ"},
            new string[]{"zhuan","×¨´«ãçžÀ×ª…¡ŒNŒŸ×©ÜžŒ£ßùˆæÉEàÄR¬ƒƒQ‹§‰t×¬âÍ×«­A®U´sÄx×­ºe¸|´uÖKÙÒN¿xÞDÏmî…×Nð‚ò§»M‡Ê÷H"},
            new string[]{"zhuang","×¯×³×±Šy×´‰ÑŽá îÇP‰ÕÇfÞÊ×®ŠÏ—[Ÿ`»’œ³Ù×˜¶´±×²¼P×°Ñbí°"},
            new string[]{"zhui","×¹›dö¿×·¸æíŠÜ×ºçÄã·®IÄJ×µÄi×¶³›®•¾Y×¸‰‹¿PÕ…á^åYåFÙ˜´œòKùxèV"},
            new string[]{"zhun","ÍÍÞ„ŒdëÆñ¸×»×¼†”ˆÍÔRƒýœÊ¶›¾MÕ"},
            new string[]{"zhuo","° æšõˆV×ÆŠƒ×¾…¬×Â×¿×Çžãí½ŸO×Àä·×½ÚÂÙ¾Q×Ã×Å×Ä†äÃ—‡—zŠß—Á—¬ìÌ”Ù”Ú×Á¬k•Œ³˜ÁM·Ÿ¸Bìú”Û˜‘“â“ð„ŸÕŽÕ}ärºWá½É”½”Þåªßª·qÖ‘ïí™·ùhè@õîÏ—ž•èCþ‰ú|»S·‡ÐX»m"},
            new string[]{"zi","×Ó×Ð…»ÆTŒI×Ö×Ô–j×Îæ¢Š—Ö¨Æ†Ãc¶f³IçÞïöÍIñè×ÑÜëÆÇSÆ“×É…è×ËŠœ×Èêß×Ê‚•„¼|–ã«R ¼í§óÊÚaâBíö±{è÷¶‡ÚÑÇç» ×Í×Õ×Ìœ¹áÑê¢æÜÈŒàt†ê†—ÂôÒ×ÏÃuÃhÙYÙDõþö¤Ô`µ›ïÅ†ïŒU×Òôôö·âˆnÉ›éC˜h·T¾l¾zÝwôüˆ÷ÚööÚƒåOÝ–ÖJÐæSætîoîpõ–õ™ùƒýUö‹Àdýb"},
            new string[]{"zong","×Ý×ÚèÈ•f×Ü¯S‚~‚ÙÌª`×Û’ÖóW¼ëê×ØÈ “K“iˆîÈŸtªf¸¾‚ôÉ~—Þ–Q³ŸôÕ·OÙ“¨ƒ¾C¾t¾h¿G¾‘¾›ÂC×ÙÛrŸÙ¼FÎx´†¯— Q¿k¿‚¿v¿å†××Û™ØqòRòiöRôAôi¼_ö`èQ"},
            new string[]{"zou","Ú[×Þ×ßæã×àÚÁÚîàYÇˆ’ô×áàu—¯—°¾j¹tÕŒÛ¸öíüPöOò|ýwý"},
            new string[]{"zu","…a×è×ç×ãÜÚü“×ä×étÙÞ×æ«~ •×â†€†XŒœ×åÝÏ½MÔ{È{—½Éaì†þ„¹ŒÛnïßæ—"},
            new string[]{"zuan","×êã@×¬ºeÀFçÚ×ëÀj„®ß¬õòèj»gÀyÜgè"},
            new string[]{"zui","¾×…‰ƒâ–èÃ½SáE•µ‘×îõþÞf·B×ï˜—ê†÷áU×íÞ©ŽT×ì™d˜áäŽå@™i‡’­rÏ`Àx"},
            new string[]{"zun","’Ä×ðƒVã†×ñ‡gýß¤é×À–¿Ÿ×Jç÷®ú•÷V"},
            new string[]{"zuo","×óÚè×÷×ô×øâôŒöŒõ…øëÑ×òìñ‚F×ùÇg¶}ßòÐŠóÐ×ö’Û×ÁÈyÈz¹iâ—ïŽ¶šàÜ´é·s¿–¼d"}
   };
        #endregion

        /// <summary>
        /// ±£´æºº×ÖµÄÊ××ÖÄ¸
        /// </summary>
        List<string> listPI = new List<string>();

        /// <summary>
        /// ±£´æºº×ÖµÄÈ«Æ´Òô
        /// </summary>
        List<string> listPinYin = new List<string>();

        /// <summary>
        /// »ñÈ¡ºº×ÖµÄÈ«Æ´Òô
        /// </summary>
        /// <param name="str">ºº×Ö×Ö·û´®</param>
        /// <returns>ºº×ÖµÄÆ´Òô</returns>
        public string Hanzi2Pinyin(string str)
        {
            string s = "";
            listPI = Convert(str, ref listPinYin);
            for (int i = 0; i < listPinYin.Count; i++)
            {
                s += listPinYin[i];
            }
            listPinYin.Clear();
            listPI.Clear();
            return s;
        }

        /// <summary>
        /// »ñÈ¡ºº×ÖµÄÊ××ÖÄ¸
        /// </summary>
        /// <param name="str">ºº×Ö×Ö·û´®</param>
        /// <returns>×ÖµÄÊ××ÖÄ¸</returns>
        public string Hanzi2PY(string str)
        {
            string s = "";
            listPI = Convert(str, ref listPinYin);
            for (int i = 0; i < listPI.Count; i++)
            {
                s += listPI[i];
            }
            listPI.Clear();
            listPinYin.Clear();
            return s;
        }

        /// <summary>
        /// ºº×Ö×ª»»³ÉÆ´Òô
        /// </summary>
        /// <param name="str">ºº×Ö×Ö·û´®</param>
        /// <param name="list">Æ´ÒôList¼¯ºÏ</param>
        /// <returns>Æ´ÒôÊ××ÖÄ¸List¼¯ºÏ</returns>
        public List<string> Convert(string str, ref List<string> list)
        {

            List<string> first = new List<string>();
            if (str == null || str == "")
                return first;
            Encoding ed = Encoding.GetEncoding("GB2312");
            if (ed == null)
                throw (new ArgumentException("Ã»ÓÐÕÒµ½±àÂë¼¯GB2312"));

            //±£´æstrÖÐ×ÖÄ¸µÄ¸öÊý
            int bAryIndex = 0;

            byte[] bAry = new byte[2];
            char[] charary1 = str.ToCharArray();


            //»ñÈ¡×ÖÄ¸µÄ¸öÊý
            for (int i = 0; i < charary1.Length; i++)
            {
                bAry = ed.GetBytes(charary1[i].ToString());
                if (bAry.Length == 1)
                {
                    bAryIndex++;
                }
            }
            //Èç¹ûÈ«ÊÇ×Ö·û´®µÄ»°Ö±½Ó·µ»Ø×Ö·û´®
            if (bAryIndex == charary1.Length)
            {
                list.Add(str);
                first.Add(str);
                return first;
            }
            //ÏÞÖÆÃû×ÖµÄ×ÖµÄ¸öÊýÎª5
            if (str.Length > 5)
            {
                str = str.Substring(0, 5);
            }
            char[] charary = str.ToCharArray();
            string[] strBlock = new string[charary.Length];

            //½«ÊäÈëµÄºº×ÖÔÚ¶þÎ»Êý×éÀïÃæÆ¥Åä
            for (int i = 0; i < charary.Length; i++)
            {
                bAry = ed.GetBytes(charary[i].ToString());
                if (bAry.Length == 1)
                {
                    strBlock[i] += charary[i].ToString() + ",";
                    bAryIndex++;
                }
                else
                {
                    for (int j = 0; j < Allhz.Length; j++)
                    {

                        if (Allhz[j][1].IndexOf(charary[i]) != -1)
                        {
                            strBlock[i] += Allhz[j][0] + ",";
                        }
                    }
                }

            }

            int temp = 0;

            List<string[]> listArray = new List<string[]>();
            for (int j = 0; j < strBlock.Length; j++)
            {
                //È¥µô×îºóÃæÄÇ¸ö¶ººÅ
                if (strBlock[j] != null)
                {
                    strBlock[j] = strBlock[j].Substring(0, strBlock[j].Length - 1);
                    listArray.Insert(temp, strBlock[j].Split(','));
                    temp = temp + 1;
                }
            }


            if (listArray.Count == 1)
            {
                first = AddPinYin1(ref list, listArray);
            }
            if (listArray.Count == 2)
            {
                first = AddPinYin2(ref list, listArray);
            }
            if (listArray.Count == 3)
            {
                first = AddPinYin3(ref list, listArray);
            }
            if (listArray.Count == 4)
            {
                first = AddPinYin4(ref list, listArray);
            }
            if (listArray.Count == 5)
            {
                first = AddPinYin5(ref list, listArray);
            }
            return first;
        }

        /// <summary>
        /// Ò»¸öºº×Ö
        /// </summary>
        /// <param name="list"></param>
        /// <param name="listArray"></param>
        /// <returns></returns>
        public List<string> AddPinYin1(ref List<string> list, List<string[]> listArray)
        {
            List<string> first = new List<string>();
            for (int j = 0; j < listArray[0].Length; j++)
            {
                list.Add(listArray[0][j]);
                first.Add(listArray[0][j].Substring(0, 1));
            }
            return first;
        }

        /// <summary>
        /// Á½¸öºº×Ö£¨ÇóÃ¿ËùÓÐ×ÖµÄµÑ¿¨¶û»ý£©
        /// </summary>
        /// <param name="list"></param>
        /// <param name="listArray"></param>
        /// <returns></returns>
        public List<string> AddPinYin2(ref List<string> list, List<string[]> listArray)
        {
            List<string> first = new List<string>();
            for (int j = 0; j < listArray[0].Length; j++)
            {
                for (int j1 = 0; j1 < listArray[1].Length; j1++)
                {
                    list.Add(listArray[0][j] + listArray[1][j1]);
                    first.Add(listArray[0][j].Substring(0, 1) + listArray[1][j1].Substring(0, 1));
                }
            }
            return first;
        }

        /// <summary>
        /// Èý¸öºº×Ö£¨ÇóÃ¿ËùÓÐ×ÖµÄµÑ¿¨¶û»ý£©
        /// </summary>
        /// <param name="list"></param>
        /// <param name="listArray"></param>
        /// <returns></returns>
        public List<string> AddPinYin3(ref List<string> list, List<string[]> listArray)
        {
            List<string> first = new List<string>();
            for (int j = 0; j < listArray[0].Length; j++)
            {
                for (int j1 = 0; j1 < listArray[1].Length; j1++)
                {
                    for (int j2 = 0; j2 < listArray[2].Length; j2++)
                    {
                        list.Add(listArray[0][j] + listArray[1][j1] + listArray[2][j2]);
                        first.Add(listArray[0][j].Substring(0, 1) + listArray[1][j1].Substring(0, 1) + listArray[2][j2].Substring(0, 1));
                    }
                }
            }
            return first;
        }

        /// <summary>
        /// ËÄ¸öºº×Ö£¨ÇóÃ¿ËùÓÐ×ÖµÄµÑ¿¨¶û»ý£©
        /// </summary>
        /// <param name="list"></param>
        /// <param name="listArray"></param>
        /// <returns></returns>
        public List<string> AddPinYin4(ref List<string> list, List<string[]> listArray)
        {
            List<string> first = new List<string>();
            for (int j = 0; j < listArray[0].Length; j++)
            {
                for (int j1 = 0; j1 < listArray[1].Length; j1++)
                {
                    for (int j2 = 0; j2 < listArray[2].Length; j2++)
                    {
                        for (int j3 = 0; j3 < listArray[3].Length; j3++)
                        {
                            list.Add(listArray[0][j] + listArray[1][j1] + listArray[2][j2] + listArray[3][j3]);
                            first.Add(listArray[0][j].Substring(0, 1) + listArray[1][j1].Substring(0, 1) + listArray[2][j2].Substring(0, 1) + listArray[3][j3].Substring(0, 1));
                        }
                    }
                }
            }
            return first;
        }

        /// <summary>
        /// Îå¸öºº×Ö£¨ÇóÃ¿ËùÓÐ×ÖµÄµÑ¿¨¶û»ý£©
        /// </summary>
        /// <param name="list"></param>
        /// <param name="listArray"></param>
        /// <returns></returns>
        public List<string> AddPinYin5(ref List<string> list, List<string[]> listArray)
        {
            List<string> first = new List<string>();
            for (int j = 0; j < listArray[0].Length; j++)
            {
                for (int j1 = 0; j1 < listArray[1].Length; j1++)
                {
                    for (int j2 = 0; j2 < listArray[2].Length; j2++)
                    {
                        for (int j3 = 0; j3 < listArray[3].Length; j3++)
                        {
                            for (int j4 = 0; j4 < listArray[4].Length; j4++)
                            {
                                list.Add(listArray[0][j] + listArray[1][j1] + listArray[2][j2] + listArray[3][j3] + listArray[4][j4]);
                                first.Add(listArray[0][j].Substring(0, 1) + listArray[1][j1].Substring(0, 1) + listArray[2][j2].Substring(0, 1) + listArray[3][j3].Substring(0, 1) + listArray[4][j4].Substring(0, 1));
                            }
                        }
                    }
                }
            }
            return first;
        }
    }
}