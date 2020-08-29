import { Component, OnInit, ElementRef, ViewChild, Input, Output } from '@angular/core';
import { HTMLNode } from '../../models/HTMLNode';
import { HTMLNodeAttribute } from '../../models/HtmlNodeAttribute';

@Component({
  selector: 'app-html-editor',
  templateUrl: './html-editor.component.html'
})
export class HtmlEditorComponent {
  @Input() content: string;

  @ViewChild('contentElement', { static: true }) contentElement: ElementRef;
  @ViewChild('visualContentHTML', { static: true }) visualContentHTML: ElementRef;
  @ViewChild('visualContent', { static: true }) visualContent: ElementRef;

  // Constants
  HTML_NODE = HTMLNode;
  TAG_DELIMITER = '{-}';
  EMPTY_DELIMITER = '';

  isTextView = true;
  queuedHTMLNode: HTMLNode;
  previousHTMLNode: HTMLNode;
  closingHTMLNodes: string[] = new Array();
  imgSrcAttr = new HTMLNodeAttribute('src');
  imgAltAttr = new HTMLNodeAttribute('alt');

  test(event: any): void {
    event.target.style.cssText = 'outline: 2px dashed silver;';

    const test: HTMLElement = document.querySelector('[cmssure]') as HTMLElement;

    if (test != null) {
      test.removeAttribute('cmssure');
      test.style.cssText = '';
    }

    event.target.setAttribute('cmssure', '');
  }

  toggleContentView(): void {
    this.isTextView = !this.isTextView;

    if (this.isTextView) {
      this.content = this.visualContent.nativeElement.innerHTML;
    }
  }

  toggleQueuedHTMLNode(queuedHTMLNode?: HTMLNode): void {
    this.queuedHTMLNode = queuedHTMLNode;
  }

  appendTag(htmlNode: HTMLNode, ...htmlNodeAttributes: HTMLNodeAttribute[]): void {
    const startPos = this.contentElement.nativeElement.selectionStart;
    const endPos = this.contentElement.nativeElement.selectionEnd;
    const selection = this.contentElement.nativeElement.selection;

    this.previousHTMLNode = htmlNode;

    const element = document.createElement(HTMLNode[htmlNode]);
    element.innerHTML = this.TAG_DELIMITER;

    if (htmlNodeAttributes != null) {
      htmlNodeAttributes.forEach((attribute) => {
        element.setAttribute(attribute.name, attribute.value);
      });
    }

    const elementNodes = element.outerHTML.split(this.TAG_DELIMITER);

    if (startPos < endPos) {
      const wrappedElement = elementNodes[0] + this.content.substr(startPos, (endPos - startPos)) + elementNodes[1];

      const preStr = this.content.substring(0, startPos);
      const postStr = this.content.substring(endPos, this.content.length);

      this.content = preStr + wrappedElement + postStr;
    } else {
      this.content += elementNodes[0];
      this.closingHTMLNodes.unshift(elementNodes[1]);
    }
  }

  closeTags(): void {
    this.content += this.closingHTMLNodes.join(this.EMPTY_DELIMITER);
    this.closingHTMLNodes = new Array();
  }
}
